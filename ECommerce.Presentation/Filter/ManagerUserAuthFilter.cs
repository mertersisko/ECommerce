using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Presentation.Filter
{
  public class ManagerUserAuthFilter : Attribute, IActionFilter
  {
    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (context.HttpContext.Session.Get("ManagerUser") == null)
        context.Result = new RedirectResult("/Admin/Login");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      if (context.HttpContext.Session.Get("ManagerUser") == null)
        context.Result = new RedirectResult("/Admin/Login");
    }
  }
}
