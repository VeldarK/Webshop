using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Core.Models;
using Webshop.DataAccess.InMemory.Repositories;

namespace Webshop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        public ProductCategoryRepository context { get; set; }

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<ProductCategory> productcategories = context.Collection().ToList();
            return View(productcategories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ProductCategory category = new ProductCategory();

            return View(category);

        }

        [HttpPost]
        public ActionResult Create(ProductCategory cat)
        {
            if (!ModelState.IsValid)
            {
                return View(cat);
            }
            else
            {
                context.Insert(cat);
                context.Commit();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var CategoryToDelete = context.FindCategory(Id);
            return View(CategoryToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var CategoryToDelete = context.FindCategory(Id);
            if (CategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var CategoryToEdit = context.FindCategory(Id);
            return View(CategoryToEdit);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory category)
        {
            context.Update(category);
            context.Commit();

            return View();
        }
    }
}