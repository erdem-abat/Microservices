using Microservices.Web.Models;
using Microservices.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microservices.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = null;

            ResponseDto? response = await _ProductService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _ProductService.CreateProductsAsync(productDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }

                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> ProductUpdate(int productId)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _ProductService.GetProductByIdAsync(productId);

                if (response != null && response.IsSuccess)
                {
                    ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(model);
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDto productDto)
        {
            ResponseDto? response = await _ProductService.UpdateProductsAsync(productDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(ProductIndex));
            }

            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDelete(int id)
        {
            TempData["success"] = "Product deleted successfully";
            await _ProductService.DeleteProductsAsync(id);
            return RedirectToAction(nameof(ProductIndex));
        }
    }
}
