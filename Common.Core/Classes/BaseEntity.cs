using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Core.Classes
{
  public class BaseEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

    [StringLength(150)]
    public string CreatedBy { get; set; }

    [StringLength(150)]
    public string LastModifiedBy { get; set; }

    public bool Active { get; set; }
    public bool Deleted { get; set; }

    [NotMapped]
    public string EnchKey { get; set; }
  }
}
