using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class OrderLine :BaseEntity ,IEntity
  {
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set;}
    public Product Product { get; set;}

    public int Quantity { get; set;}
    public decimal UnitPrice { get; set;}
    public int VatRate { get; set;}
  }
}
