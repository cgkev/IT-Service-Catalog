using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace IT_product_log.Models
{
    public class CategoryModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter a Category")]
        public string Category { get; set; }
    }
}