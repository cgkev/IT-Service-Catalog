using IT_product_log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.SharePoint.Client;

namespace IT_product_log
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public class UserProfilePictureActionFilter : ActionFilterAttribute
        {

            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                string name = System.Web.HttpContext.Current.User.Identity.Name;

                filterContext.Controller.ViewBag.userName = name;

             
            }

        }

        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new UserProfilePictureActionFilter(), 0);

            RouteConfig.RegisterRoutes(RouteTable.Routes);


        }
    }
}