using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Classes;

namespace ECommerce.Domain.Entities
{
  public class Order : BaseEntity
  {
    //Data-000001
    //Data-000002

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public string OrderNo { get; set; }

    public DateTime OrderDate { get; set; }

    public ICollection<OrderLine> OrderLines { get; set; }
  }
}
