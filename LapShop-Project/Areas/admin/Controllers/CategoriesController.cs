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
        public IActionResult List()
        {
            try
            {
                return View(oClsCategories.GetAll());
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the categories.");
                return View(); // Optionally, use a custom error view.
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int? categoryId)
        {
            try
            {
                var category = new TbCategory();
                if (categoryId != null)
                    category = oClsCategories.GetById(Convert.ToInt32(categoryId));

                return View(category);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the category.");
                return View(new TbCategory()); // Returning an empty category object as fallback.
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, IFormFile Files)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Edit", category);

                category.ImageName = await ClsUiHelper.UploadImage(Files, "Uploads/Categories");
                oClsCategories.Save(category);

                return RedirectToAction("List");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the category.");
                return View("Edit", category);
            }
        }

        [HttpGet]
        public IActionResult Delete(int CategoryId)
        {
            try
            {
                oClsCategories.Delete(CategoryId);
                return RedirectToAction("List");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the category.");
                return RedirectToAction("List");
            }
        }
    }
}
