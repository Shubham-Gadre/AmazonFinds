using AmazonFinds.Data;
using AmazonFinds.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonFinds.Repository
{
    public class ProductRepository
    {
        private readonly ProductContext _productContext = null;
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public async Task<int> AddNewProduct(ProductModel model)
        {
            var newProduct = new ProductEntities
            {
                ProductName=model.ProductName,
                ProductLink=model.ProductLink,
                CreatedOn=DateTime.Now,
                ProductImageUrl=model.ProductImageUrl,

            };
            await  _productContext.AddAsync(newProduct);
            await _productContext.SaveChangesAsync();
            return newProduct.Id;
        }
        public async Task<List<ProductModel>> GetAllProducts() 
        {
            var products = new List<ProductModel>();
            var allProducts = await _productContext.Products.ToListAsync();
            if (allProducts?.Any()==true)
            {
                foreach (var item in allProducts)
                {
                    products.Add(new ProductModel()
                    {
                        ProductName=item.ProductName,
                        ProductLink=item.ProductLink,
                        CreatedOn=item.CreatedOn,
                        UpdatedOn=item.UpdatedOn,
                        ProductImageUrl=item.ProductImageUrl,   

                    });

                }
            }
            return products;
        }

        public async Task DeleteProduct(int passedId)
        {
            var ProductToBeDeleted = await _productContext.Products.FindAsync(passedId);

            
            if (ProductToBeDeleted != null)
            {
                _productContext.Remove(ProductToBeDeleted);
                await _productContext.SaveChangesAsync();
            }
        }
        
    }
}
