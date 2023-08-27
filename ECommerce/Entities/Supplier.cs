using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class Supplier : BaseEntity, IEntity
  {
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }

    public int TownId { get; set; }
    public Town Town { get; set; }

    public string Mail { get; set; }
    public decimal BalanceStatus { get; set; }

  }
}
