namespace ECommerce.Domain.Dtos
{
  public class CityDto
  {
    public string il_adi { get; set; }
    public List<TownDto> ilceler { get; set; }
  }
}
