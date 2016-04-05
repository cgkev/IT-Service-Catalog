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

    }
}
