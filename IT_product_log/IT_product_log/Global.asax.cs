using IT_product_log.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IT_product_log
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static void VerifyDatabaseExists()
        {
            using (var context = new Context())
            {
                context.Database.CreateIfNotExists();
            }
        }
        protected void Application_Start()
        {
            VerifyDatabaseExists();
            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
