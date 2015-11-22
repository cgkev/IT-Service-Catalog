using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IT_product_log.Models;
using IT_product_log.Model_Maps;



namespace IT_product_log
{
    public class Context : DbContext
    {
        public Context()
            : base("name=IT")
        {

        }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<Model> Model { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ModelMap());
            modelBuilder.Configurations.Add(new CategoryMap());

        }

    }
}