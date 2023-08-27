using Microsoft.Extensions.Localization;

namespace Common.Resource.Interfaces
{
  public interface IResourceService
  {
    LocalizedString GetLocalizedText(string key);

    LocalizedString GetLocalizedText(string key, params object[] args);
  }
}
