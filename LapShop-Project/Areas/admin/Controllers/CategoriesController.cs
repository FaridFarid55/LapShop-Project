

namespace LapShop_Project.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin,Owner,Data Entry")]
    [Area("admin")]
    public class CategoriesController : Controller
    {
        private readonly ILapShop<TbCategory> oClsCategories;
        public CategoriesController(ILapShop<TbCategory> Category)
        {
            oClsCategories = Category;
        }

        [HttpGet]
        public IActionResult list()
        {
            return View(oClsCategories.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int? categoryId)
        {
            var Category = new TbCategory();
            if (categoryId != null)
                Category = oClsCategories.GetById(Convert.ToInt32(categoryId));

            return View(Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", category);

            category.ImageName = await ClsUiHelper.UploadImage(Files, "Categories");
            oClsCategories.Save(category);

            return RedirectToAction("list");
        }

        [HttpGet]
        public IActionResult Delete(int CategoryId)
        {
            oClsCategories.Delete(CategoryId);

            return RedirectToAction("list");
        }
    }
}
