﻿using IT_product_log.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using Microsoft.SharePoint.Client;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System;
using System.Linq;
using SPClient = Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint;
namespace IT_product_log.Controllers

{

    public class PortalController : Controller
    {
        public ViewResult Index()
        {
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            Session["CurrentUserName"] = name;

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
            SpConnectionVPN spConnection = new SpConnectionVPN();
            List<VpnRequest> storage = spConnection.getMyRequests();
            ViewBag.list = storage;
            return View();
        }

        [HttpGet]
        public ViewResult MyRequest(int id)
        {
            //Need to talk to Ignacio and Kevin about passing back ViewBag.list from before - not a huge priority, jus a small efficiancy issue 
            SpConnectionVPN spConnection = new SpConnectionVPN();
            List<VpnRequest> storage = spConnection.getMyRequests();
            ViewBag.id = id;

            //the id in the parameter is the request id : the id used on our sharepoint site, to get true id, subtract 1000 
            foreach(VpnRequest current in storage)
            {
                if (current.VPN_requestID == id)
                {
                    ViewBag.details = current;
                    return View();
                }
            }
            return View();
        }
        // -----------------Review Request (manager view)-------------
        public ViewResult ReviewRequests()
        {
            SpConnectionVPN spConncetion = new SpConnectionVPN();
            List<VpnRequest> storage = spConncetion.getPendingRequests();
            ViewBag.list = storage;
            return View();
        }

        [HttpGet]
        public ViewResult ReviewRequest(int id)
        {
            SpConnectionVPN spConnection = new SpConnectionVPN();
            List<VpnRequest> storage = spConnection.getPendingRequests();
            ViewBag.id = id;

            //the id in the parameter is the request id : the id used on our sharepoint site
            foreach(VpnRequest current in storage)
            {
                if (current.VPN_requestID == id)
                {
                    ViewBag.details = current;
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ReviewRequest(int id, string submit, string comments)
        {
            //based on previous code, submit can be checked with submit.Equals("Approve") 

            //implementation of this will be in spConnection, just to stay consistent
            SpConnectionVPN spConnection = new SpConnectionVPN();
            spConnection.ReviewRequest(id, submit, comments);

            return RedirectToAction("/ReviewRequests", "Portal");
        }

        //------------------------IT added fields for the form and submission----------------------------------------------------------

        [HttpGet]
        public ViewResult ReviewRequestIT(int id)
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.id = id;
            ViewBag.details = storage[id - 1001];
            ViewBag.radiusSelector = new string[] { "", "Full Access", "QTC Web Access", "R CRM Contractor", "R Indexer Remote", "R LMCO Support", "R Neudesic Contractor", "R QA Remote", "R SP Portal Contractor", "R Telehealth", "REVPN QTC Transcribers", "Other" };

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

    }
}

