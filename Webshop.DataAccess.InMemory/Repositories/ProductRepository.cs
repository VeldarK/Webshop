using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Webshop.Core.Models;

namespace Webshop.DataAccess.InMemory.Repositories
{
    public class ProductRepository
    {

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.FirstOrDefault(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                int index = products.FindIndex(p => p.Id == productToUpdate.Id);
                products[index] = product;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public Product FindProduct(string Id)
        {
            Product product = products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product ProductToDelete = FindProduct(Id);
            products.Remove(ProductToDelete);
        }
        
    }
}
