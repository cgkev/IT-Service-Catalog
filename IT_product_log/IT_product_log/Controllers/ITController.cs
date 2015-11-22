using IT_product_log.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IT_product_log.Controllers

{

    public class ITController : Controller
    {
       
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult ViewInventory()
        {
            using (var context = new Context())
            {
                List<Model> list = new List<Model>();
                foreach (var item in context.Model)
                {
                    Model temp = new Model
                    {
                        id = item.id,
                        Code = item.Code,
                        Name = item.Name,
                        Category = item.Category,
                        Description = item.Description                        
                    };
                    list.Add(temp);
                }
                ViewBag.list = list;
            }
            return View();
        }


        public ViewResult ViewItem(int id)
        {
            using (var context = new Context())
            {
                List<Model> list = new List<Model>();
                foreach (var item in context.Model)
                {
                    Model temp = new Model
                    {
                        id = item.id,
                        Code = item.Code,
                        Name = item.Name,
                        Category = item.Category,
                        Description = item.Description
                    };
                    list.Add(temp);
                }
                ViewBag.list = list;
            }
            ViewBag.id = id;
            return View();
        }



        [HttpGet]
        public ViewResult AddItem()
        {
            List<Model> list = new List<Model>();

            using (var context = new Context())
            {
                foreach (var item in context.Model)
                {
                    Model temp = new Model
                    {
                        id = item.id,
                        Code = item.Code,
                        Name = item.Name,
                        Category = item.Category,
                        Description = item.Description
                    };
                    list.Add(temp);
                }
                ViewBag.list = list;
            }

           
            using (var context = new Context())
            {
                List<CategoryModel> catList = new List<CategoryModel>();
                foreach (var cat in context.Category)
                {
                    CategoryModel temp = new CategoryModel
                    {
                        id = cat.id,
                        Category = cat.Category
                    };
                    catList.Add(temp);
                }
                ViewBag.catList = catList;
            }
            //pass id to determine next id# in view
            if (list.Count == 0)
            {
                ViewBag.id = 1;
            }
            else
            {
                ViewBag.id = list[list.Count - 1].id + 1;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddItem(Model input)
        {
            List<Model> list = new List<Model>();

            using (var context = new Context())
            {
                foreach (var item in context.Model)
                {
                    Model temp = new Model
                    {
                        id = item.id,
                        Code = item.Code,
                        Name = item.Name,
                        Category = item.Category,
                        Description = item.Description
                    };
                    list.Add(temp);
                }
                ViewBag.list = list;
            }

            using (var context = new Context())
            {
                List<CategoryModel> catList = new List<CategoryModel>();
                foreach (var cat in context.Category)
                {
                    CategoryModel temp = new CategoryModel
                    {
                        id = cat.id,
                        Category = cat.Category
                    };
                    catList.Add(temp);
                }
                ViewBag.catList = catList;
            }

            //checks if code is exists 
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Code.Equals(input.Code))
                {
                    ModelState.AddModelError(string.Empty, "Code exist already!");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                return View(input);
            }

            else //all requirements are passed, and item is added 
            {
                using (var context = new Context())
                {
                    var toAdd = new Model()
                    {
                        Code = input.Code,
                        Name = input.Name,
                        Category = input.Category,
                        Description = input.Description
                    };

                    context.Model.Add(toAdd);

                    context.SaveChanges();
                }


                return RedirectToAction("/ViewInventory", "IT");
            }
        }

        [HttpGet]
        public ViewResult AddCategory()
        {   
            using (var context = new Context())
            {
                List<CategoryModel> catList = new List<CategoryModel>();
                foreach (var cat in context.Category)
                {
                    CategoryModel temp = new CategoryModel
                    {
                        id = cat.id,
                        Category = cat.Category
                    };
                    catList.Add(temp);
                }
                ViewBag.catList = catList;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel input)
        {
            using (var context = new Context())
            {
                var category = new CategoryModel()
                {
                    Category = input.Category
                };

                context.Category.Add(category);

                context.SaveChanges();
            }
            return RedirectToAction("/AddCategory", "IT");        
        }
    }
}