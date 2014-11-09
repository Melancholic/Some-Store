using SomeStore.Models.Abstract;
using SomeStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SomeStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private int PageSize = 3;
        public ProductController(IProductRepository productRepo)
        {
            repository = productRepo;
        }

        //For lists view
        public ViewResult List(int page=1)
        {
            ProductsListViewModel viewModel = new ProductsListViewModel();
            viewModel.Products = repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            viewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage=page,
                ItemsPerPage=PageSize,
                TotalItems=repository.Products.Count()
            };
            return View(viewModel);
        }

    }
}
