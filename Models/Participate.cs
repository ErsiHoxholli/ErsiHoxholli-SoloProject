#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BetaLottery.Models;

public class Participate
{
    [Key]
    public int ParticipateId {get; set;}

    public int ProductId {get; set;}
    public int ClientId {get; set;}
    public Product? MyProduct {get; set;}     
    public Client? MyClient {get; set;}     

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

      
    }