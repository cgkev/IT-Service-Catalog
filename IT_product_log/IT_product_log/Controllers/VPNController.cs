﻿using System;
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