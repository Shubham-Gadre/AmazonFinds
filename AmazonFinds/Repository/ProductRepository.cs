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
                        UpdatedOn=item.UpdatedOn

                    });

                }
            }
            return products;
        }

        public ProductModel GetProductById( int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        private List<ProductModel> DataSource()
        {
            return new List<ProductModel>()
            {
                new ProductModel(){ Id=1,ProductName="Phone", ProductLink="Link2"},
                new ProductModel(){ Id=2,ProductName="TV", ProductLink="Link3"},
                new ProductModel(){ Id=3,ProductName="Oven", ProductLink="Link4"},
                new ProductModel(){ Id=4,ProductName="Jwellery", ProductLink="Link5"},
                new ProductModel(){ Id=5,ProductName="Dumbell", ProductLink="Link6"},
            };
        }
    }
}
