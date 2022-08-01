using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace EmployeeSystem.Web.Util
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(filterContext.HttpContext.Session.GetString("Token")))
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Logout");
                redirectTargetDictionary.Add("controller", "Account");
                redirectTargetDictionary.Add("area", "");

                //filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                filterContext.Result = new RedirectToActionResult("Logout", "Account", null);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
