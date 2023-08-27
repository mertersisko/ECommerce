using ECommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Common.Core.Classes;
using Common.Core.Interfaces;
using Common.Helper.Extentions;
using Common.Resource.Resources;

namespace ECommerce.Domain.Entities;

public class Customer : BaseEntity, IEntity
{
  [StringLength(100)]
  [LocalizedDisplayName("Name", NameResourceType = typeof(Resource))]
  public string Name { get; set; }

  [StringLength(100)]
  [LocalizedDisplayName("Surname", NameResourceType = typeof(Resource))]
  public string Surname { get; set; }


  [StringLength(100)]
  [LocalizedDisplayName("EMail", NameResourceType = typeof(Resource))]
  public string EMail { get; set; }

  [StringLength(250)]
  [LocalizedDisplayName("Password", NameResourceType = typeof(Resource))]
  public string Password { get; set; }


  [StringLength(20)]
  [LocalizedDisplayName("PhoneNumber", NameResourceType = typeof(Resource))]
  public string PhoneNumber { get; set; }


  public Gender Gender { get; set; }

  public ICollection<CustomerAdress> CustomerAdresses { get; set; }
}