using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_product_log.Models
{
    public class VpnRequest
    {
        //TODOOOOOOOOOOO....................

        // the fields below I need to find out what are the fields that I am going to expect and how am I going to grab these
        // for now ill make them all strings except those that I believe are int'a and other stuff.

        public string Company_Name { get; set; }// Company Name here but what values?
        public string Company_Other { get; set; }// What is this meant to hold?
        public string Machine_Owner { get; set; }// values meant to hold
        public string Manager { get; set; }// okay this uses LDAP?
        public string Office_Address { get; set; }// the address?
        public string Office_Location { get; set; }//isnt this the same as address?
        public string Radius_Profile_Other { get; set; }//Need to figure what this is for
        public string Radius_Profile_Select { get; set; }// this one is a ditto
        public string Systems_List { get; set; }//this one is also a Ditto
        public DateTime VPN_accessEnd { get; set; }// date on this one
        public DateTime VPN_accessStart { get; set; }// same as the one above
        public string VPN_justification { get; set; }//I think this is a paragraph
        public string VPN_profileSelect { get; set; }//how are we getting these fields
        public string VPN_recipientEmail { get; set; }//?? is the requestor typing this??
        public string VPN_recipientFirst { get; set; }//?? is the requestor typing this??
        public string VPN_recipientLast { get; set; }//??is the requestor typing this??
        public int VPN_requestID { get; set; }//?? how is the ID grabed?
        public string VPN_requestStatus { get; set; }//whats this?
        public int VPN_userCode { get; set; }//how do we grab this or what are the values?
        public string VPN_userDept { get; set; }// okay this is a 3 digit code.
        public string VPN_userStatus { get; set; }// is this hired.
        public PhoneAttribute Work_Phone { get; set; }//phone of course
        public string VPN_requestor { get; set; }//


        //this is from kevin old code might need but not for now :D
        //
        //public int id { get; set; }
        //[Required(ErrorMessage = "Please enter a code")]
        //public string Code { get; set; }
        //[Required(ErrorMessage = "Please enter a name")]
        //public string Name { get; set; }
        //public string Category { get; set;}
        //public string Description { get; set; }
    }
}