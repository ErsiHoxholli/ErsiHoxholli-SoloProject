#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BetaLottery.Models;

public class PromoterUser
{
    [Key]
    public int PromoterUserId {get; set;}

    [Required]
    [MinLength (2, ErrorMessage ="First Name must be 2 Characters minimum")]
    [Display(Name = "Your First Name")]
    public string FirstName {get; set;}

    [Required]
    [MinLength (2, ErrorMessage ="First Name must be 2 Characters minimum")]
    [Display(Name = "Your Last Name")]
    public string LastName {get; set;}


    [Required]
    [Display(Name = "Your Username")]
    public string UserName {get; set;}

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage =" Password must be at least 8 Characters")]
    public string Password {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public List<Product> CreatedProducts { get; set; } = new List<Product>(); 

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Confirm Your Password")]
    public string Confirm {get; set;}

    }

public class LoginPromoterUser 
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