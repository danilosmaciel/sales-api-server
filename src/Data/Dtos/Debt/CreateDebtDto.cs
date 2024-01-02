

using System.ComponentModel.DataAnnotations;

namespace SalesApi.src.Data.Dtos;

public class CreateDebtDto{

    [Required(ErrorMessage = "O valor do débito é obrigatório")]
    public int value { get; set; }

    [Required]
    public int CustomerId { get; set; }

}