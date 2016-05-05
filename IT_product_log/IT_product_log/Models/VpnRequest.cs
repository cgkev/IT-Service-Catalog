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

        // Grabbed Data
        public int VPN_requestID { get; set; }//?? how is the ID grabed?
        public string VPN_requestor { get; set; }//
        public string VPN_requestStatus { get; set; }//whats this?

        public string DateSubmitted { get; set; }

        // -----------FORM Fields-----------
        [Required]
        [Display(Name = "First name")]
        public string VPN_recipientFirst { get; set; }//?? is the requestor typing this??Yes

        [Required]
        [Display(Name = "Last name")]
        public string VPN_recipientLast { get; set; }//??is the requestor typing this??Yes

        [Required]
        public string VPN_userStatus { get; set; }// is this hired.

        [Required]
        public string VPN_userDept { get; set; }// okay this is a 3 digit code.

        [Required]
        [Display(Name = "ex: 999")]
        public int VPN_userCode { get; set; }//how do we grab this or what are the values?

        [Required]
        [Display(Name = "ex: (555) 555-5555")]
        public String Work_Phone { get; set; }//phone of course

        [Required]
        [Display(Name = "ex: user@qtcm.com")]
        public string VPN_recipientEmail { get; set; }//?? is the requestor typing this??Yes

        [Required]
        public string Company_Name { get; set; }// Company Name here but what values  

        [Required]
        [Display(Name = "Enter other company name")]
        public string Company_Other { get; set; }// 

        [Required]
        public string Office_Location { get; set; }//isnt this the same as address

        [Required]
        public string Office_Address { get; set; }// the address 

        [Required]
        public string Machine_Owner { get; set; }// values meant to hold

        [Required]
        [Display(Name = "Please provide details on what systems to be access through VPN")]
        public string Systems_List { get; set; }//this one is also a Ditto

        [Required]
        [Display(Name = "Please provide detailed business jusification for VPN access")]
        public string VPN_justification { get; set; }//I think this is a paragraph

        [Required]
        [Display(Name = "Start Date")]
        public string VPN_accessEnd { get; set; }// this used to be DateTime I hope nothing breaks

        [Required]
        [Display(Name = "End Date")]
        public string VPN_accessStart { get; set; }// this used to be DateTime I hope nothing breaks

        [Required]
        public string Manager { get; set; }// okay this uses LDAP?
                                           //------------New updates to this model-----------------------------------------

        [Display(Name = "Ext: 999")]
        public int Ext_code { get; set; }

        [Display(Name = " ")]
        public string Agency { get; set; }

        public string Reviewer_Comments { get; set; }

        //-------------end of new updates-----------------------------------------------
        // -----------IT Manager Approval Fields-----------
        public string VPN_profileSelect { get; set; }
        public string Radius_Profile_Select { get; set; }
        public string Radius_Profile_Other { get; set; }


    }
}