
using System.ComponentModel.DataAnnotations;
using Common.Core.Classes;


namespace ECommerce.Domain.Entities;

public class CustomerAdress : BaseEntity 
{
  public int CustomerId { get; set; }
  public Customer Customer { get; set; }

  public int TownId { get; set; }
  public Town Town { get; set; }

  [StringLength(100)]
  public string Definition { get; set; }


  [StringLength(400)]
  public string Adress { get; set; }
}