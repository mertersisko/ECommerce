using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Classes;
using Common.Core.Interfaces;

namespace ECommerce.Domain.Entities
{
  public class Campaign : BaseEntity, IEntity
  {
    public int ProductId { get; set; }
    public string CampaignName { get; set; }
    public DateTime FirstDate { get; set; }
    public DateTime LastDate { get; set; }
    public int CampaignType { get; set; }
    public decimal FirstPrice { get; set; }
    public decimal SecondPrice { get;set; }

  }
}
