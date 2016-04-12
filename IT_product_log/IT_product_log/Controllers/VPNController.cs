using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_product_log.Models;


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
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            storage.Add(input);

            //int var = storage.Count;//Just used this to see if the request was sent in
            return RedirectToAction("/ThankYou", "Portal");
        }

        public ViewResult VPNfaq()
        {
            return View();
        }

        //for testing 
        [HttpGet]
        public ActionResult GetManager()
        {
            var hell1o = Request.Params["id"];
            Console.Write(hell1o);

            //this one contains users
            var data = new
            {
                items = new[] {
                new { key = 1, firstname = "Lex", lastname = "Luther", username = "LuLex" },
                new { key = 2, firstname = "Bruce", lastname = "Wayne", username = "NotBatman"},
                new { key = 2, firstname = "Johnny", lastname = "Bravo", username = "PretyBoy"},
                new { key = 2, firstname = "Nicholas", lastname = "Cage", username = "NichCage"}
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
