#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BetaLottery.Models;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;

public class Product
{
    [Key]
    public int ProductId {get; set;}

    [Required]
    [MinLength (2, ErrorMessage ="First Name must be 2 Characters minimum")]
    [Display(Name = "Name of Product")]
    public string NameOfProduct {get; set;}

    [Required]
    [MinLength (2, ErrorMessage ="First Name must be 2 Characters minimum")]
    [Display(Name = "Description of Product")]
    public string Description {get; set;}
//     public string? Myimage { get; set; }

// [NotMapped]
//     public IFormFile Image { get; set; }

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public int PromoterUserId {get; set;}
    public List<Participate> Participates { get; set; } = new List<Participate>(); 


    public PromoterUser? MyPromotion {get; set;}        
    }