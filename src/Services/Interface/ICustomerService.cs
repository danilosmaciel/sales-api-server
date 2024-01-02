
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace SalesApi.src.Services.Interface;

public interface ICustomerService
{
    public ResponseCustomerDto GetAll();

    public ResponseCustomerDto GetByNameFilter(string filter);

    public ReadCustomerDto? GetById(int id);

    public ResponseCustomerDto GetByRange(int page,int size);

    public Customer AddCustomer(CreateCustomerDto dto);

    public Customer? Update(int id, UpdateCustomerDto dto);

    public Customer? PartialUpdate(int id, JsonPatchDocument<UpdateCustomerDto> patch, ControllerBase controllerBase);

    public Customer? Delete(int id);
}