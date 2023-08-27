using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.Core.Extentions;

public static class SessionExtentions
{
  public static void Set<T>(this ISession session, string key, T value)
  {
    var settings = new JsonSerializerSettings
    {
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    session.SetString(key, JsonConvert.SerializeObject(value, settings));
  }

  public static T? Get<T>(this ISession session, string key)
  {
    var value = session.GetString(key);

    var settings = new JsonSerializerSettings
    {
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    return value == null ? default :
      JsonConvert.DeserializeObject<T>(value, settings);
  }
}