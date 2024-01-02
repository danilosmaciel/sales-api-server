using System.ComponentModel.DataAnnotations;

using SalesApi.src.Data.Dtos;

public class ResponseCustomerDto{

    public virtual ICollection<ReadCustomerDto>? Registers { get; set; }

    public int TotalCount { get; set; }

    public int TotalAmountDebts { get; set; }

}