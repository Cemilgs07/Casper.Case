using Casper.UILayer.Options;
using CasperUI.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace Casper.UILayer.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        public CategoryController(
            IHttpClientFactory httpClientFactory,
            IOptions<ApiSettings> apiOptions,
            ILogger<CategoryController> logger)
            : base(httpClientFactory, apiOptions, logger)
        {
        }
        public async Task<IActionResult> CategoryList()
        {
            var categories = await GetList<CategoryDto>("Category/GetAll");
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var category = await GetItem<UpdateCategoryDto>($"Category/GetById/{id}");

            if (category == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("CategoryList");
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var success = await PutItem($"Category/Update/{dto.Id}", dto);

            if (success)
            {
                return RedirectToAction("CategoryList");
            }

            ModelState.AddModelError("", "Kategori güncellenirken bir hata oluştu.");
            return View(dto);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new CreateCategoryDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var success = await PostItem("Category/Create", dto);

            if (success)
            {
                return RedirectToAction("CategoryList");
            }

            ModelState.AddModelError("", "Kategori eklenirken bir hata oluştu.");
            return View(dto);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await DeleteItem($"Category/Delete/{id}");

            if (!success)
            {
                TempData["Error"] = "Ürün silinirken bir hata oluştu.";
            }

            return RedirectToAction("CategoryList");
        }

    }
}
