using Common.Resource.Interfaces;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Common.Resource.Classes
{
  public class ResourceService : IResourceService
  {
    private readonly IStringLocalizer _localizer;

    public ResourceService(IStringLocalizerFactory factory)
    {
      var type = typeof(Resources.Resource);
      var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
      _localizer = factory.Create("Resource", assemblyName.Name);
    }

    public LocalizedString GetLocalizedText(string key)
    {
      return _localizer[key];
    }

    public LocalizedString GetLocalizedText(string key, params object[] args)
    {
      return _localizer[key, args];
    }
  }
}
