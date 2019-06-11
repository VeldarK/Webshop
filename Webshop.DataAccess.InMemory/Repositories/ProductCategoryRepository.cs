using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Webshop.Core.Models;

namespace Webshop.DataAccess.InMemory.Repositories
{
    public class ProductCategoryRepository
    {

        public ProductCategoryRepository()
        {
            productcategories = cache["productcategories"] as List<ProductCategory>;
            if (productcategories == null)
            {
                productcategories = new List<ProductCategory>();
            }
        }
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcategories;

        public void Commit()
        {
            cache["productcategories"] = productcategories;
        }

        public void Insert(ProductCategory category)
        {
            productcategories.Add(category);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory productToUpdate = productcategories.FirstOrDefault(p => p.Id == category.Id);

            if (productToUpdate != null)
            {
                int index = productcategories.FindIndex(p => p.Id == productToUpdate.Id);
                productcategories[index] = category;
            }
            else
            {
                throw new Exception("Category not found.");
            }
        }

        public ProductCategory FindCategory(string Id)
        {
            ProductCategory category = productcategories.FirstOrDefault(p => p.Id == Id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new Exception("Category not found.");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productcategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory CategoryToDelete = FindCategory(Id);
            productcategories.Remove(CategoryToDelete);
        }
    }
}
