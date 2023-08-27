using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class ProductImages : BaseEntity, IEntity
  {
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string ImagePath { get; set; }
  }
}
