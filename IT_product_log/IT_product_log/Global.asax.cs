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

            VpnRequest approved1 = new VpnRequest
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
                VPN_requestID = 1001,
                VPN_requestStatus = "Approved",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

            };

            VpnRequest denied1 = new VpnRequest
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
                VPN_requestID = 1002,
                VPN_requestStatus = "Pending Manager Approval",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

            };

            VpnRequest pending1 = new VpnRequest
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
                VPN_requestID = 1003,
                VPN_requestStatus = "Rejected by Manager",
                VPN_userCode = 3,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

            };

            VpnRequest approved2 = new VpnRequest
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
                VPN_requestID = 1004,
                VPN_requestStatus = "Pending Security Manager Approval",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

            };

            VpnRequest denied2 = new VpnRequest
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
                VPN_requestID = 1005,
                VPN_requestStatus = "Rejected by Security Manager",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

            };

            VpnRequest pending2 = new VpnRequest
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
                VPN_requestID = 1006,
                VPN_requestStatus = "Pending IT Manager Approval",
                VPN_userCode = 3,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

            };

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
                VPN_requestID = 1007,
                VPN_requestStatus = "Rejected by IT Manager",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

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
                VPN_requestID = 1008,
                VPN_requestStatus = "Rejected by Security Manager",
                VPN_userCode = 123,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

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
                VPN_requestID = 1009,
                VPN_requestStatus = "Pending Manager Approval",
                VPN_userCode = 3,
                VPN_userDept = "123???String",
                VPN_userStatus = "Nope",
                VPN_requestor = "what goes here",
                Work_Phone = "323-343-1234",
                VPN_accessStart = "04/12/2011",
                VPN_accessEnd = "04/12/2012"

            };

            vpnRequests.Add(approved1);
            vpnRequests.Add(denied1);
            vpnRequests.Add(pending1);
            vpnRequests.Add(approved2);
            vpnRequests.Add(denied2);
            vpnRequests.Add(pending2);
            vpnRequests.Add(approved);
            vpnRequests.Add(denied);
            vpnRequests.Add(pending);
            Application["vpnList"] = vpnRequests;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}