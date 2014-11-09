using SomeStore.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SomeStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IProductRepository repository;
        public NavigationController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Menu(string category=null)
        {
            ViewBag.CurrentCat = category;
            IEnumerable<string> categories = repository.Products
                    .Select(x => x.Category)
                    .Distinct()
                    .OrderBy(x => x);
            return View(categories);
        }

    }
}
