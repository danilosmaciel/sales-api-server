
using System.ComponentModel.DataAnnotations;

namespace SalesApi.src.Data.Dtos;

public class CreateUserDto{

    [Required]
    public required string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=(.*\d){2,})(?=.*[^A-Za-z0-9]).{6,}$", 
        ErrorMessage = "A senha deve possuir ao menos uma letra maiúscula, 2 números, caracter especial e 8 caracteres"
    )]
    public required string Password { get; set; }

    [Required]
    [Compare("Password")]
    public required string RePassword { get; set; }

}