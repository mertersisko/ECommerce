using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Enums;

public enum Gender : byte
{
  [Display(Name = "Erkek")]
  Male = 1,
  [Display(Name = "Kadın")]
  Female = 2,
  [Display(Name = "Diğer")]
  Other = 3,
}