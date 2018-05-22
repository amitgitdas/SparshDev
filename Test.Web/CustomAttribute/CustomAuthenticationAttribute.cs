using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Filters;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace SparshWeb.CustomAttribute
{
    public interface IAuthenticationFilter
    {
        void OnAuthentication(AuthenticationContext filterContext);

        void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext);
    }

    public class CustomAuthenticationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string name = filterContext.HttpContext.User.Identity.Name;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

            }
            else
            {
                // Otherwise redirect to your specific authorized area
                filterContext.Result = new RedirectResult("~/Admin/SetLogin");
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var areaName = filterContext.RouteData.DataTokens["area"];
                if (areaName.Equals("Admin"))
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Admin", action = "Login", area = "Admin" }));
                }
                else if (areaName.Equals("Public"))
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Admin", action = "Login" }));
                }
                // other conditions...

            }
        }
    }
}