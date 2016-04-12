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
            //these are the values for the form 
            ViewBag.vpnStatusType = new string[] { "", "QTC Regular Employee", "Temp Employee", "Contractor", "Consultant", "Transcriber", "QTC Provider", "LMCO Employee(Non-QTC)" };
            ViewBag.deptName = new string[] { "", "CLS", "CRP", "IT", "STS", "VAS", "VHA" };
            ViewBag.companyName = new string[] { "", "QTC", "LMCO", "Other" };
            ViewBag.officeLoc = new string[] { "", "QTC Admin", "QTC Clinic", "Other" };
            ViewBag.qtcOfficSelect = new string[] {"", "Diamond Bar, CA - 21700 Copley Dr. ",
                "Diamond Bar, CA - 1440 Bridgegate Dr.", "San Antonio, TX - 4400 NW Loop 410",
                "Philadelphia, PA - 1617 JFK. Blvd." };
            ViewBag.machineOwner = new string[] { "", "QTC Owned PC", "Company", "Asset (Non-QTC)", "Personal PC" };

            return View();
        }

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
            }};
            String[] hello = new string[0];
            //this should be an empty array in other words nothing found.
            var data1 = new { items = hello };


            //just change between the two values data1 or data to see the empty array sent in bellow
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult VPNRequest(VpnRequest input)
        {
            //Too do need to add the form validation here.
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];

            input.VPN_requestStatus = "Pending Manager Approval";
            input.DateSubmitted = DateTime.Now.ToString("M/d/yyyy");
            input.VPN_requestID = storage[storage.Count - 1].VPN_requestID + 1;


            storage.Add(input);
            //int var = storage.Count;//Just used this to see if the request was sent in
            return RedirectToAction("/ThankYou", "Portal");
        }

        public ViewResult VPNfaq()
        {
            return View();
        }

    }
}
