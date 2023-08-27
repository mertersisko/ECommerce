using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class City : BaseEntity, IEntity

  {
    public string Definition { get; set; }
    public ICollection<Town> Towns { get; set; }
  }
}
