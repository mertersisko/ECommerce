using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class Product : BaseEntity, IEntity
  {
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockAmount { get; set; }
    public string Color { get; set; }
    public ICollection<ProductImages> ProductImages { get; set; }
  }
}
