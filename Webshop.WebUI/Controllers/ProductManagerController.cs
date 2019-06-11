using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Core.Models;
using Webshop.Core.ViewModels;
using Webshop.DataAccess.InMemory.Repositories;

namespace Webshop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        ProductRepository productContext;
        ProductCategoryRepository productCategoryContext;

        public ProductManagerController()
        {
            productContext = new ProductRepository();
            productCategoryContext = new ProductCategoryRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Product> products = productContext.Collection().ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductManagerVM viewModel = new ProductManagerVM();
            viewModel.ProductCategories = productCategoryContext.Collection();
            viewModel.Product = new Product();
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                productContext.Insert(product);
                productContext.Commit();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var productToDelete = productContext.FindProduct(Id);
            return View(productToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var productToDelete = productContext.FindProduct(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                productContext.Delete(Id);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var productToEdit = productContext.FindProduct(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            ProductManagerVM viewModel = new ProductManagerVM();
            viewModel.Product = productToEdit;
            viewModel.ProductCategories = productCategoryContext.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productContext.Update(product);
            productContext.Commit();

            return View();
        }



    }

}