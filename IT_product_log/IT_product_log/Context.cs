using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IT_product_log.Models;



namespace IT_product_log
{
    public class Context : DbContext
    {
        public Context()
            : base("name=IT")
        {

        }
   

    }
}