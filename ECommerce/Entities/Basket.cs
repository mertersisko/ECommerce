using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class Basket : BaseEntity, IEntity
  {
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public bool Status { get; set; }
    public ICollection<BasketLine>BasketLines { get; set; }

  }

}
