using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class Town : BaseEntity, IEntity
  {
    public int CityId { get; set; }
    public City City { get; set; }
    public string Definition { get; set; }
  }
}
