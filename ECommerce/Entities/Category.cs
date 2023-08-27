using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Classes;
using Common.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Domain.Entities;

public class Category : BaseEntity, IEntity
{
  public int? ParentCategoryId { get; set; }
  public Category ParentCategory { get; set; }


  [StringLength(150)]
  public string Definition { get; set; }

  [StringLength(300)]
  public string ShortDescription { get; set; }

  [StringLength(40)]
  public string CoverPhoto { get; set; }

  [NotMapped]
  public IFormFile File { get; set; }
}