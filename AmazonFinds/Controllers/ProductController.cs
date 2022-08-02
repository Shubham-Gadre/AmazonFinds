using AmazonFinds.Models;
using AmazonFinds.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonFinds.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController( ProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
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

            if (_productModel.ProductImage!=null)
            {
                string folder = "products/images/";
                folder += Guid.NewGuid().ToString() + _productModel.ProductImage.FileName;
                _productModel.ProductImageUrl = "/" + folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await _productModel.ProductImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));


            }
            if (ModelState.IsValid)
            {
                int id = await _productRepository.AddNewProduct(_productModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(ProductAddEditPage), new { isSuccess = true });
                }
            }
            
            return View("ProductAddEditPage");
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


        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductModel model)
        {
            var simpleData = await _productRepository.GetAllProducts();
            if (model.Id!=0)
            {
                await _productRepository.DeleteProduct(model.Id);
                var data = await _productRepository.GetAllProducts();
                if (data != null)
                {
                    ViewBag.productDeleted = true;
                    ViewBag.isDeleteEnable = true;
                    return View("Index", data);
                }
            }

            return View ("Index", simpleData);

        }
    }
}
