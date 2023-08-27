using System.Globalization;
using System.Text;
using Common.Helper.Enums;

namespace Common.Helper.Helpers
{
  public static class UrlHelper
  {
    public static string CreateUrl(string content, Language language)
    {
      string url = content.ToLower();
      switch (language)
      {
        case Language.Turkish:
          url = url
            .Replace("ğ", "g")
            .Replace("ü", "u")
            .Replace("ş", "s")
            .Replace("ı", "i")
            .Replace("ö", "o")
            .Replace("ç", "c");
          break;
        case Language.English:
          url = url
            .Replace("ğ", "g")
            .Replace("ü", "u")
            .Replace("ş", "s")
            .Replace("ı", "i")
            .Replace("ö", "o")
            .Replace("ç", "c");
          break;
        case Language.Germany:
          url = url
            .Replace("ä", "ae")
            .Replace("ö", "oe")
            .Replace("ü", "ue")
            .Replace("ß", "ss");
          break;
        case Language.French:
          url = url
            .Replace("é", "e")
            .Replace("è", "e")
            .Replace("ê", "e")
            .Replace("ë", "e")
            .Replace("à", "a")
            .Replace("â", "a")
            .Replace("ô", "o")
            .Replace("ù", "u")
            .Replace("û", "u")
            .Replace("î", "i")
            .Replace("ï", "i")
            .Replace("ç", "c");
          break;
        default:
          url = RemoveDiacritics(url);
          break;
      }

      url = url.Replace(' ', '-');

      // Remove invalid characters
      url = RemoveInvalidUrlCharacters(url);

      return url;
    }

    static string RemoveDiacritics(string text)
    {
      var normalizedString = text.Normalize(NormalizationForm.FormD);
      var stringBuilder = new StringBuilder();

      foreach (var c in normalizedString)
      {
        var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
        {
          stringBuilder.Append(c);
        }
      }

      return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    static string RemoveInvalidUrlCharacters(string text)
    {
      var invalidChars = new[] { "?", "/", "\\", ",", ".", ":", ";", "@", "&", "=", "+", "$", "#", "%", "<", ">", "!", "\"", " ", "{", "}", "|", "^", "[", "]", "`", "(", ")", "'" };
      foreach (string invalidChar in invalidChars)
      {
        text = text.Replace(invalidChar, "");
      }
      return text;
    }
  }
}
