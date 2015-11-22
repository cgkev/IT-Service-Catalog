using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using IT_product_log.Models;

// Install-Package EntityFramework 

namespace IT_product_log.Model_Maps
{
    public class ModelMap : EntityTypeConfiguration<Model>
    {
        public ModelMap()
        {
            Property(p => p.Code).IsRequired();
            Property(p => p.Name).IsRequired();
        }
    }
}