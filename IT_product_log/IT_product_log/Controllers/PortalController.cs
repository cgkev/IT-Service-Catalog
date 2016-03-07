using IT_product_log.Models;
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

        public ViewResult Service_Desk()
        {
            return View();
        }

        public ViewResult System_Access()
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

        // -------------Start of Info FAQ Views-------------

        public ViewResult VPNfaq()
        {
            return View();
        }
        // -----------------MyRequest-----------------

        public ViewResult MyRequest()
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.list = storage;
            return View();
        }

        [HttpGet]
        public ViewResult MyRequestView(int id)
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.id = id;
            ViewBag.details = storage[id - 1];
            return View();
        }

        // -----------------Review Request (manager view)-------------
        public ViewResult ReviewRequest()
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.list = storage;
            return View();
        }

        [HttpGet]
        public ViewResult ReviewRequestView(int id)
        {
            List<VpnRequest> storage = (List<VpnRequest>)HttpContext.Application["vpnList"];
            ViewBag.id = id;
            ViewBag.details = storage[id - 1];
            return View();
        }

        [HttpPost]
        public ActionResult ReviewRequestView(string status, int id)
        {
            
            return RedirectToAction("/ReviewRequest", "Portal");

        }


        // -----------------Old Code-------------------


        public ViewResult ViewInventory()
        {
            List<Model> storage = (List<Model>)HttpContext.Application["list"];
            ViewBag.list = storage;
            return View();
        }
        public ViewResult ViewItem(int id)
        {
            List<Model> storage = (List<Model>)HttpContext.Application["list"];
            ViewBag.list = storage;
            ViewBag.id = id;
            return View();
        }

        [HttpGet]
        public ViewResult AddItem()
        {
            List<Model> storage = (List<Model>)HttpContext.Application["list"];
            ViewBag.id = storage[storage.Count - 1].id + 1;
            List<string> catList = (List<string>)HttpContext.Application["catList"];

            ViewBag.catList = catList;

            return View();
        }

        [HttpPost]
        public ActionResult AddItem(Model input)
        {
            List<Model> storage = (List<Model>)HttpContext.Application["list"];
            ViewBag.id = storage[storage.Count - 1].id + 1;

            List<string> catList = (List<string>)HttpContext.Application["catList"];
            ViewBag.catList = catList;

            bool codeExist = false;
            for (int i = 0; i < storage.Count; i++)
            {
                if (storage[i].Code.Equals(input.Code))
                {
                    codeExist = true;
                }
            }

            if (string.IsNullOrEmpty(input.Code) || string.IsNullOrEmpty(input.Name) || codeExist)
            {

                if (string.IsNullOrEmpty(input.Code))
                {
                    ModelState.AddModelError(string.Empty, "Code is required!");
                }
                if (string.IsNullOrEmpty(input.Name))
                {
                    ModelState.AddModelError(string.Empty, "Name is required!");
                }

                if (codeExist)
                {
                    ModelState.AddModelError(string.Empty, "Code already exist!");
                }
                //return RedirectToAction("/AddItem", "IT");
                return View(input);
            }

            else
            {
                input.id = storage[storage.Count - 1].id + 1;
                storage.Add(input);
                return RedirectToAction("/ViewInventory", "IT");
            }
        }

        [HttpGet]
        public ViewResult CategoryView()
        {
            List<Model> storage = (List<Model>)HttpContext.Application["list"];
            ViewBag.id = storage[storage.Count - 1].id + 1;
            return View();
        }

        [HttpPost]
        public ActionResult CategoryView(string category)
        {
            List<string> catList = (List<string>)HttpContext.Application["catList"];
            catList.Add(category);

            return RedirectToAction("/ViewInventory", "IT");
        }


    }
}