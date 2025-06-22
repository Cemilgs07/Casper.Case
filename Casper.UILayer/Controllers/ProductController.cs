using Casper.UILayer.Options;
using CasperUI.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace Casper.UILayer.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        public ProductController(
           IHttpClientFactory httpClientFactory,
           IOptions<ApiSettings> apiOptions,
           ILogger<ProductController> logger)
           : base(httpClientFactory, apiOptions, logger)
        {
        }
        public async Task<IActionResult> ProductList()
        {
            var products = await GetList<ProductDto>("Product/GetAll");

            
            return View(products);
        }

       
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await GetList<CategoryDto>("Category/GetAll");

            var dto = new CreateProductDto
            {
                Categories = categories
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await PostItem("Product/Create", dto);

            if (success)
                return RedirectToAction("ProductList");

            ModelState.AddModelError("", "Ürün eklenirken bir hata oluştu.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var product = await GetItem<UpdateProductDto>($"Product/GetById/{id}");
            if (product == null)
            {
                TempData["Error"] = "Ürün bulunamadı.";
                return RedirectToAction("ProductList");
            }

            var categories = await GetList<CategoryDto>("Category/GetAll");
            product.Categories = categories;

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await PutItem($"Product/Update/{dto.Id}", dto);

            if (success)
                return RedirectToAction("ProductList");

            ModelState.AddModelError("", "Ürün güncellenirken bir hata oluştu.");
            return View(dto);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var success = await DeleteItem($"Product/Delete/{id}");

            if (!success)
            {
                TempData["Error"] = "Ürün silinirken bir hata oluştu.";
            }

            return RedirectToAction("ProductList");
        }
    }
}

