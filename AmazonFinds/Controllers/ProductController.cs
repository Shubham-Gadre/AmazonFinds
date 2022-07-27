using AmazonFinds.Models;
using AmazonFinds.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonFinds.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository = null;
        public ProductController( ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index(bool isDeleteEnable=false)
        {
            ViewBag.isDeleteEnable = isDeleteEnable;
            var allProductsObject=await _productRepository.GetAllProducts();
            return View("Index",allProductsObject);
        }

        public ViewResult ProductAddEditPage(bool isSuccess=false)
        {
            ViewBag.isSuccess = isSuccess;
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductModel _productModel)
        {
            if (_productModel.ProductName!="")
            {
                int id = await _productRepository.AddNewProduct(_productModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(ProductAddEditPage), new { isSuccess = true });
                }
            }
            return View();
        }

        public ViewResult Admin()
        {
            return View();
        }

        public async Task<ViewResult> GetAllProducts()
        {
            var data = await _productRepository.GetAllProducts();
            return View("Index",data);
        }

        public IActionResult DeleteFromAdminPage()
        {
            return RedirectToAction("Index", new { isDeleteEnable=true });
        }
    }
}
