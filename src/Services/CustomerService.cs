

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using SalesApi.src.Data;
using SalesApi.src.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.Configuration.Conventions;
namespace SalesApi.src.Services;


public class CustomerService: ICustomerService{

    private readonly ILogger<CustomerService> _logger;
    private SalesContext _context;
    private IMapper _mapper;


    public CustomerService(ILogger<CustomerService> logger, SalesContext context, IMapper mapper){
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public ResponseCustomerDto GetAll() {
        var customers = _context
        .Customers
        .Where(dto => dto.Active == 1)
        .OrderByDescending(c => c.Debts != null ? c.Debts.First(d => d.PaidAt != null).Value : 0
        ).ToList();

        int totalDebts = customers
                            .Where(a => a.Debts != null)
                            .SelectMany(b => b.Debts!)
                            .Where(c => c.PaidAt != null)
                            .Sum(d => d.Value);
        
        var dto = _mapper.Map<List<ReadCustomerDto>>(customers).ToList();
        var responseDto = new ResponseCustomerDto
        {
            Registers = dto,
            TotalCount = dto.Count,
            TotalAmountDebts = totalDebts
        };

        return responseDto;
    }

    public ResponseCustomerDto GetByNameFilter(string filter)
    {
         var customers = _context
        .Customers
        .Where(dto => dto.Active == 1 && dto.FullName.ToLower().Contains(filter.ToLower()))
        .OrderByDescending(c => c.Debts!.First(d => d.PaidAt == null).Value )
        .ToList();
        
        int totalDebts = customers
                            .Where(a => a.Debts != null)
                            .SelectMany(b => b.Debts!)
                            .Where(c => c.PaidAt != null)
                            .Sum(d => d.Value);
        
        var dto = _mapper.Map<List<ReadCustomerDto>>(customers).ToList();
        var responseDto = new ResponseCustomerDto
        {
            Registers = dto,
            TotalCount = dto.Count,
            TotalAmountDebts = totalDebts
        };

        return responseDto;
    }

    public ReadCustomerDto? GetById(int id) {
        Customer? f = _context.Customers.FirstOrDefault((f) => f.Id == id);

        if(f==null || f.Active == 0){
            return null;
        }
        return _mapper.Map<ReadCustomerDto>(f);
    }

    public ResponseCustomerDto GetByRange(int page, int limit){
        var totalCustomers = _context
        .Customers
        .Where(dto => dto.Active == 1)
        .OrderByDescending(c => c.Debts!.First(d => d.PaidAt == null).Value )
        .ToList();
        
         int totalDebts = totalCustomers
                            .Where(a => a.Debts != null)
                            .SelectMany(b => b.Debts!)
                            .Where(c => c.PaidAt == null)
                            .Sum(d => d.Value);

        var customers = totalCustomers.Skip((page - 1) * limit).Take(limit).ToList();
                     
        var dtos = _mapper.Map<List<ReadCustomerDto>>(customers).ToList();

        dtos.ForEach(d => {
            d.TotalAmountDebts = d.Debts != null ? d.Debts.Where(d => d.PaidAt == null).Sum(d => d.Value) : 0;
        });

        var responseDto = new ResponseCustomerDto
        {
            Registers = dtos,
            TotalCount = totalCustomers.Count,
            TotalAmountDebts = totalDebts
        };

        return responseDto;
    }

    public Customer AddCustomer(CreateCustomerDto dto){
        try{
            var customer = _mapper.Map<Customer>(dto);

            Console.WriteLine($"CLiente = {customer.ToString()}");
            _context.Customers.Add(customer);
            _context.SaveChanges();  
            return customer;
        }catch (Exception e) {
            _logger.LogError(e, "Erro ao salvar o Customer");
            throw new ApplicationException("Ocorreum um erro na criação do cliente!");
        }   
    }

    public Customer? Update(int id, UpdateCustomerDto dto){
       Customer? f = _context.Customers.FirstOrDefault((f) => f.Id == id);

        if(f==null || f.Active == 0){
            return null;
        }

        _mapper.Map(dto, f);
        _context.SaveChanges();  

        return f;
    }

    public Customer? PartialUpdate(int id, JsonPatchDocument<UpdateCustomerDto> patch, ControllerBase controllerBase){
        Customer? f = _context.Customers.FirstOrDefault((f) => f.Id == id);

        if(f==null || f.Active == 0){
            return null;
        }

        var customerUpdate = _mapper.Map<UpdateCustomerDto>(f);
        patch.ApplyTo(customerUpdate, controllerBase.ModelState);

        if(!controllerBase.TryValidateModel(customerUpdate)){
            return null;
        }

         _mapper.Map(customerUpdate, f);
         _context.SaveChanges(); 
         return f; 
    }

    public Customer? Delete(int id){
        Customer? f = _context.Customers.FirstOrDefault((f) => f.Id == id);

        if(f==null || f.Active == 0){
            return null;
        }

        var dto = _mapper.Map<UpdateCustomerDto>(f);
        dto.Active = 0;
        
        return Update(id, dto);
    }

    
}