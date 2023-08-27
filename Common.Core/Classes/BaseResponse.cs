using Common.Core.Enums;

namespace Common.Core.Classes;

public class BaseResponse<T>
{
  public string Title { get; set; }
  public string Message { get; set; }
  public ResultStatus ResultStatus { get; set; }
  public T Data { get; set; }
  public IList<T> DataList { get; set; }
  public string Url { get; set; }
}