using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using IT_product_log.Models;

namespace IT_product_log.Model_Maps
{
    public class CategoryMap : EntityTypeConfiguration<CategoryModel>
    {
        public CategoryMap()
        {
            Property(p => p.Category).IsRequired();
        }
    }
}