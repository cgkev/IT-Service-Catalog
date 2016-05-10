using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_product_log.Models;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace IT_product_log.Controllers
{
    public class VPNController : Controller
    {

        [HttpGet]
        public ViewResult VPNRequest()
        {
            //fetching values for the form (sharepoint client object model)  

            SpConnectionVPN spConnect = new SpConnectionVPN();
            ViewBag.vpnStatusType = spConnect.getVpnStatusTypeChoices();
            ViewBag.deptName = spConnect.getDeptNameChoices();
            ViewBag.companyName = spConnect.getCompanyNameChoices();
            ViewBag.officeLoc = spConnect.getQtcOfficeLocationChoices();
            ViewBag.qtcOfficSelect = spConnect.getQtcOfficeSelectChoices();
            ViewBag.machineOwner = spConnect.getMachineOwnerChoices();
       
            return View();
        }


        [HttpPost]
        public ActionResult VPNRequest(VpnRequest input)
        {
            //formating some input - kevin/ignacio's code
            input.VPN_requestStatus = "Pending Manager Approval";
            input.DateSubmitted = DateTime.Now.ToString("M/d/yyyy");
            //input.VPN_requestID = storage[storage.Count - 1].VPN_requestID + 1;

            //update SP with the data gathered - I will keep application scope data here for now in case we need it 
            //deleted the ID increment, will be done automatically by SharePoint 

            SpConnectionVPN spConnect = new SpConnectionVPN();
            spConnect.addRequest(input);

            //To do need to add the form validation here.
            //List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            //storage.Add(input);

            //int var = storage.Count;//Just used this to see if the request was sent in
            return RedirectToAction("/ThankYou", "Portal");
        }

        public ViewResult VPNfaq()
        {
            return View();
        }

        //for testing - still a work in progress, not ready yet 
        [HttpGet]
        public ActionResult GetManager()
        {
            //This came from Ignacio's code, I am assumign it is the name the user has given as a search tool 
            var search = Request.Params["id"];

            System.Diagnostics.Debug.WriteLine(search.Split(' ')[0]);
            System.Diagnostics.Debug.WriteLine(search.Split(' ')[1]);

            PrincipalContext prinCon = new PrincipalContext(ContextType.Domain);

            UserPrincipal query = new UserPrincipal(prinCon);
            query.GivenName = search.Split(' ')[0];
            query.Surname = search.Split(' ')[1];

            System.Diagnostics.Debug.WriteLine(query.GivenName);
            System.Diagnostics.Debug.WriteLine(query.Surname);

            PrincipalSearcher searcher = new PrincipalSearcher(query);
            List<String> firstName = new List<String>();
            List<String> lastName = new List<String>();
            List<String> userName = new List<String>();

            foreach (UserPrincipal result in searcher.FindAll())
            {
                firstName.Add(result.GivenName);
                lastName.Add(result.Surname);
                userName.Add(result.UserPrincipalName);
            };


            //data contains an array of result users
            var data = new
            {
                items = new[] {
                new { key = 1, firstname = firstName[0], lastname = lastName[0], username = userName[0] },
                new { key = 2,  firstname = firstName[1], lastname = lastName[1], username = userName[1]},
                new { key = 3,  firstname = firstName[2], lastname = lastName[2], username = userName[2]}
             }
            };

            


            String[] hello = new string[0];
            //this should be an empty array in other words nothing found.
            var data1 = new { items = hello };


            //just change between the two values data1 or data to see the empty array sent in bellow
            return Json(data, JsonRequestBehavior.AllowGet);
        
        }
    }
}
