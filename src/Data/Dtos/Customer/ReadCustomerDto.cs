using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SalesApi.src.Data.Dtos;
using SalesApi.src.Models;

public class ReadCustomerDto{
    
    public int Id { get; set; }

    public required string FullName { get; set; }

    public required string Cpf { get; set; }

    public DateOnly DateBirth { get; set; }

    public String? Email { get; set; }

    [JsonIgnore]
    public virtual ICollection<ReadDebtDto>? Debts { get; set; }

    public int Active { get; set; }

    public int TotalAmountDebts { get; set; }

}