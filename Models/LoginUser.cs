#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BetaLottery.Models;

public class LoginUser 
{
[Required]
[Display(Name = "Your Username")]
public string UserName {get; set;}

[Required]
[DataType(DataType.Password)]
[MinLength(8, ErrorMessage =" Password must be at least 8 Characters")]
[Display(Name = "Enter Your Password")]
public string Password {get; set;}
}