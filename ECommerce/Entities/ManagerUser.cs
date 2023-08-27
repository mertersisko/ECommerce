using System.ComponentModel.DataAnnotations;
using Common.Core.Classes;
using Common.Helper.Extentions;
using Common.Resource.Resources;

namespace ECommerce.Domain.Entities
{
  public class ManagerUser : BaseEntity
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

    [StringLength(30)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(Resource))]
    public string Password { get; set; }

    public string NameAndSurName => $"{Name} {Surname}";

  }
}
