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
        public string Company_Name { get; set; }
        public string Company_Other { get; set; }
        public string Machine_Owner { get; set; }
        public string Manager { get; set; }
        public string Office_Address { get; set; }
        public string Office_Location { get; set; }
        public string Radius_Profile_Other { get; set; }
        public string Radius_Profile_Select { get; set; }
        public string Systems_List { get; set; }
        public DateTime VPN_accessEnd { get; set; }
        public DateTime VPN_accessStart { get; set; }
        public string VPN_justification { get; set; }
        public string VPN_profileSelect { get; set; }
        public string VPN_recipientEmail { get; set; }
        public string VPN_recipientFirst { get; set; }
        public string VPN_recipientLast { get; set; }
        public int VPN_requestID { get; set; }
        public string VPN_requestStatus { get; set; }
        public int VPN_userCode { get; set; }
        public string VPN_userDept { get; set; }
        public string VPN_userStatus { get; set; }
        public PhoneAttribute Work_Phone { get; set; }
        public string VPN_requestor { get; set; }



        public int id { get; set; }
        [Required(ErrorMessage = "Please enter a code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        public string Category { get; set;}
        public string Description { get; set; }
    }
}