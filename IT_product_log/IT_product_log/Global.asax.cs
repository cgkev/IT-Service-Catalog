using IT_product_log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IT_product_log
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            List<Model> productList = new List<Model>();

            List<string> categoryList = new List<string>();
            categoryList.Add("Laptop");
            categoryList.Add("Computer");
            Application["catList"] = categoryList;



            Model item1 = new Model
            {
                id = 1,
                Code = "LT_00001",
                Category = "Laptop",
                Name = "Apple Macbook Pro 13-inch",
                Description = "8gb ram i5 2.5ghz"
            };

            Model item2 = new Model
            {
                id = 2,
                Code = "PC_00001",
                Category = "Computer",
                Name = "Dell Vostro Tower",
                Description = "i7 5690X 6-core 64gb ram"
            };

            Model item3 = new Model
            {
                id = 3,
                Code = "LT_00002",
                Category = "Laptop",
                Name = "Dell XPS",
                Description = "i7 8gb ram"
            };


            productList.Add(item1);
            productList.Add(item2);
            productList.Add(item3);



            Application["list"] = productList;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}