using System.ComponentModel.DataAnnotations;

namespace Common.Core.Enums;

public enum ResultStatus
{
  [Display(Name = "Başarılı")]
  Success = 1,
  [Display(Name = "Bilgilendirme")]
  Info = 2,
  [Display(Name = "Uyarı")]
  Warning = 3,
  [Display(Name = "Hata")]
  Error = 4,
}