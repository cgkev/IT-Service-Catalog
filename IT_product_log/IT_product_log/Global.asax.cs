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
            //VPN request model loaded :D
            List<VpnRequest> vpnRequests = new List<VpnRequest>();

            VpnRequest approved = new VpnRequest
            {
                DateSubmitted = "01-23-2001",
                Company_Name = "QTC",
                Company_Other = "QTC",
                Machine_Owner = "Yes",
                Manager = "Kevin",
                Office_Address = "123 Elm st",
                Office_Location = "QTC Head Quarters",
                Radius_Profile_Other = "Null",
                Radius_Profile_Select = "Null",
                Systems_List = "Hello World",
                VPN_profileSelect = "Null",
                VPN_justification = "Reasons",
                VPN_recipientEmail = "test123@gmail.com",
                VPN_recipientFirst = "Ignacio",
                VPN_recipientLast = "Zuniga",
                VPN_requestID = 1,
                VPN_requestStatus = "Approved",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here"

            };

            VpnRequest denied = new VpnRequest
            {
                DateSubmitted = "04-14-2014",
                Company_Name = "QTC",
                Company_Other = "QTC",
                Machine_Owner = "Yes",
                Manager = "Kevin",
                Office_Address = "123 Elm st",
                Office_Location = "QTC Head Quarters",
                Radius_Profile_Other = "Null",
                Radius_Profile_Select = "Null",
                Systems_List = "Hello World",
                VPN_profileSelect = "Null",
                VPN_justification = "Reasons",
                VPN_recipientEmail = "test123@gmail.com",
                VPN_recipientFirst = "Ignacio",
                VPN_recipientLast = "Zuniga",
                VPN_requestID = 2,
                VPN_requestStatus = "Denied",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here"

            };

            VpnRequest pending = new VpnRequest
            {
                DateSubmitted = "01-27-2016",
                Company_Name = "QTC",
                Company_Other = "QTC",
                Machine_Owner = "Yes",
                Manager = "Kevin",
                Office_Address = "123 Elm st",
                Office_Location = "QTC Head Quarters",
                Radius_Profile_Other = "Null",
                Radius_Profile_Select = "Null",
                Systems_List = "Hello World",
                VPN_profileSelect = "Null",
                VPN_justification = "Reasons",
                VPN_recipientEmail = "test123@gmail.com",
                VPN_recipientFirst = "Ignacio",
                VPN_recipientLast = "Zuniga",
                VPN_requestID = 3,
                VPN_requestStatus = "Pending Manager Approval",
                VPN_userCode = 3,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here"

            };

            vpnRequests.Add(approved);
            vpnRequests.Add(denied);
            vpnRequests.Add(pending);



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

            Application["vpnList"] = vpnRequests;
            Application["list"] = productList;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}