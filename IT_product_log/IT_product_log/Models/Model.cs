using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_product_log.Models
{
    public class Model
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Please enter a code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        public string Category { get; set;}
        public string Description { get; set; }
    }
}