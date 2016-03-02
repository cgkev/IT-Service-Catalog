﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IT_product_log.Models;
using System.Web.Mvc;

namespace IT_product_log.Controllers
{
    public class FormsController : Controller
    {
        [HttpGet]
        public ViewResult VPNRequest()
        {
            //these are the values for the form 
            ViewBag.vpnStatusType = new string[] { "QTC Regular Employee", "Temp Employee", "Contractor", "Consultant", "Transcriber", "QTC Provider", "LMCO Employee(Non-QTC)" };
            ViewBag.deptName = new string[] { "CLS", "CRP", "IT", "STS", "VAS", "VHA" };
            ViewBag.companyName = new string[] { "QTC", "LMCO", "Other" };
            ViewBag.officeLoc = new string[] { "QTC Admin", "QTC Clinic", "Other" };
            ViewBag.qtcOfficSelect = new string[] { "Diamond Bar", "Diamand Bar2", "San Antonio", "Philadelphia" };
            ViewBag.machineOwner = new string[] { "QTC Owned PC", "Company", "Asset (Non-QTC)", "Personal PC" };

            //this is the model
            return View();
        }

        [HttpPost]
        public ActionResult VPNRequest(VpnRequest input)
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            storage.Add(input);
            //int var = storage.Count;
            return RedirectToAction("/ThankYou");
        }
    }
}
