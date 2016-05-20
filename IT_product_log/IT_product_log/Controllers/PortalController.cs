using IT_product_log.Models;
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
            return View();
        }

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

        public ViewResult ThankYou()
        {
            return View();
        }

        public ViewResult ReviewerThankYou()
        {
            return View();
        }

        public ViewResult MyRequests()
        {
            SpConnectionVPN spConnection = new SpConnectionVPN();

            ViewBag.RequestAll = spConnection.getAllMyRequests();
            ViewBag.RequestRejected = spConnection.getRejectedMyRequests();
            ViewBag.RequestApproved = spConnection.getApprovedMyrequests();
            ViewBag.RequestPending = spConnection.getPendingMyRequests();

            return View();
        }

        [HttpGet]
        public ViewResult MyRequest(int id)
        {
            SpConnectionVPN spConnection = new SpConnectionVPN();
            ViewBag.details = spConnection.getRequestById(id);
            ViewBag.id = id;

            return View();
        }

        public ViewResult ReviewRequests()
        {
            //ReviewAll is currently not being show; it is hidden in the views. 

            SpConnectionVPN spConncetion = new SpConnectionVPN();
            ViewBag.ReviewPending = spConncetion.getPendingReviews();
            ViewBag.ReviewApproved = spConncetion.getApprovedReviews();
            ViewBag.ReviewRejected = spConncetion.getRejectedReviews();
            ViewBag.ReviewAll = spConncetion.getAllReviews();

            return View();
        }

        [HttpGet]
        public ViewResult ReviewRequest(int id)
        {
            SpConnectionVPN spConnection = new SpConnectionVPN();
            List<VpnRequest> storage = spConnection.getPendingReviews();
            ViewBag.id = id;

            //the id in the parameter is the request id : the id used on our sharepoint site
            foreach(VpnRequest current in storage)
            {
                if (current.VPN_requestID == id)
                {
                    //check if the request is in final IT manager step
                    if (current.VPN_requestStatus.Equals("Pending IT Manager Approval"))
                    {
                        //if the request is in the final IT manager step, send to ReviewRequestIT 
                        return ReviewRequestIT(current);
                    }
                    ViewBag.details = current;
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ReviewRequest(int id, string submit, string comments)
        {
            SpConnectionVPN spConnection = new SpConnectionVPN();
            spConnection.ReviewRequest(id, submit, comments);

            return RedirectToAction("/ReviewerThankYou", "Portal");
        }

        [HttpGet]
        public ViewResult ReviewRequestIT(VpnRequest current)
        {
            ViewBag.details = current;
            SpConnectionVPN spConnection = new SpConnectionVPN();
            ViewBag.radiusSelector = spConnection.getVpnRadiusProfileSelect();

            return View("ReviewRequestIT");
        }

        [HttpPost]
        public ActionResult ReviewRequestIT(int id, string submit, string comments, string VPN_Radius, string VPN_Other, string VPN_accessStart, string VPN_accessEnd, string[] checkboxes)
        {
            //checkboxes represent the VPN profile selection. Minimum size 1, max size 2. 

            SpConnectionVPN spConnection = new SpConnectionVPN();
            spConnection.ReviewRequest(id, submit, comments, VPN_Radius, VPN_Other, VPN_accessStart, VPN_accessEnd, checkboxes);

            return RedirectToAction("/ReviewerThankYou", "Portal");
        }
    }
}

