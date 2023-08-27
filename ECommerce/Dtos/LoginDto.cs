using System.ComponentModel;

namespace ECommerce.Domain.Dtos;

public class LoginDto
{
  [DisplayName("E-Mail")]
  public string EMail { get; set; }

  [DisplayName("Parola")]
  public string Password { get; set; }
}