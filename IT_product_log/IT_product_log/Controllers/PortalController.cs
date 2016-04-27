﻿using IT_product_log.Models;
using System.Collections.Generic;
using System.Web.Mvc;
namespace IT_product_log.Controllers

{

    public class PortalController : Controller
    {

        public ViewResult Index()
        {
            return View();
        }

        // -------------Start of Portal Views-------------

        public ViewResult ServiceDesk()
        {
            return View();
        }

        public ViewResult SystemAccess()
        {
            return View();
        }

        public ViewResult Management()
        {
            return View();
        }
        
        public ViewResult ViewManagementPortal()
        {
            return View();
        }

        // -------------Thank You Page-------------

        public ViewResult ThankYou()
        {
            return View();
        }

       
        // -----------------MyRequest-----------------

        public ViewResult MyRequests()
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.list = storage;
            return View();
        }

        [HttpGet]
        public ViewResult MyRequest(int id)
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.id = id;
            ViewBag.details = storage[id - 1001];
            return View();
        }
        // -----------------Review Request (manager view)-------------
        public ViewResult ReviewRequests()
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.list = storage;
            return View();
        }

        [HttpGet]
        public ViewResult ReviewRequest(int id)
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.id = id;
            ViewBag.details = storage[id - 1001];

            return View();
        }

        //------------------------IT added fields for the form and submission----------------------------------------------------------

        [HttpGet]
        public ViewResult ReviewRequestIT(int id)
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.id = id;
            ViewBag.details = storage[id - 1001];
            ViewBag.radiusSelector = new string[] { "", "Full Access", "QTC Web Access", "R CRM Contractor", "R Indexer Remote", "R LMCO Support", "R Neudesic Contractor", "R QA Remote", "R SP Portal Contractor", "R Telehealth" , "REVPN QTC Transcribers" , "Other" };

            return View();
        }

        [HttpPost]
        public ActionResult ReviewRequestIT(int id, string submit, string comments)
        {
            System.Diagnostics.Debug.WriteLine("param " + id + " " + submit + " " + comments);




            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            for (int i = 0; i < storage.Count; i++)
            {
                if (storage[i].VPN_requestID == id)
                {
                    if (submit.Equals("Approve"))
                        storage[i].VPN_requestStatus = "Approved";
                    else
                        storage[i].VPN_requestStatus = "Denied";

                    System.Diagnostics.Debug.WriteLine("asdasd " + storage[i].VPN_requestID + " " + storage[i].VPN_requestStatus);

                }
            }



            return RedirectToAction("/ReviewRequests", "Portal");
        }

        //------------------------End of IT added fields for the form and submission----------------------------------------------------------

        [HttpPost]
        public ActionResult ReviewRequest(int id, string submit, string comments)
        {
            System.Diagnostics.Debug.WriteLine("param " + id + " " + submit + " " + comments);
          
               


            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            for(int i = 0; i < storage.Count; i++)
            {
                if(storage[i].VPN_requestID == id)
                {
                    if(submit.Equals("Approve"))
                    storage[i].VPN_requestStatus = "Approved";
                    else
                        storage[i].VPN_requestStatus = "Denied";

                    System.Diagnostics.Debug.WriteLine("asdasd "+ storage[i].VPN_requestID + " " + storage[i].VPN_requestStatus);

                }
            }



            return RedirectToAction("/ReviewRequests", "Portal");
        }


       


    }
}

