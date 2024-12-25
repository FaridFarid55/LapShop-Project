namespace LapShop_Project.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin,Data Entry,Owner")]
    [Area("admin")]
    public class ItemsController : Controller
    {
        private readonly Iitems _itemService;
        private readonly ILapShop<TbCategory> _categoryService;
        private readonly ILapShop<TbO> _osService;
        private readonly ILapShop<TbItemType> _itemTypeService;

        // Constructor
        public ItemsController(Iitems itemService, ILapShop<TbCategory> categoryService,
            ILapShop<TbO> osService, ILapShop<TbItemType> itemTypeService)
        {
            _itemService = itemService;
            _categoryService = categoryService;
            _osService = osService;
            _itemTypeService = itemTypeService;
        }

        [HttpGet]
        public IActionResult List(int? itemId)
        {
            try
            {
                ViewBag.ListCategorie = _categoryService.GetAll();
                var items = itemId != null
                    ? _itemService.GetAllItemsData(itemId)
                    : _itemService.GetAllItemsData(null);
                return View(items);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the items. Please try again.");
                return View(new List<VwItem>());
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int? itemId)
        {
            try
            {
                ViewBag.listCategories = _categoryService.GetAll();
                ViewBag.lstItemTypes = _itemTypeService.GetAll();
                ViewBag.lstOs = _osService.GetAll();

                var item = itemId != null ? _itemService.GetById(Convert.ToInt32(itemId)) : new TbItem();
                return View(item);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the item. Please try again.");
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(nameof(Edit), item);
            }

            try
            {
                item.ImageName = await ClsUiHelper.UploadImage(files, "Uploads/Items");
                _itemService.Save(item);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the item. Please try again.");
                PopulateDropdowns();
                return View(nameof(Edit), item);
            }
        }

        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Delete(int itemId)
        {
            try
            {
                _itemService.Delete(itemId);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the item. Please try again.");
                return RedirectToAction(nameof(List));
            }
        }

        public IActionResult Search(int id)
        {
            try
            {
                ViewBag.ListCategorie = _categoryService.GetAll();
                var items = _itemService.GetAllItemsData(id);
                return View(nameof(List), items);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while searching for items. Please try again.");
                return RedirectToAction(nameof(List));
            }
        }

        // Helper method to populate dropdown lists
        private void PopulateDropdowns()
        {
            ViewBag.listCategories = _categoryService.GetAll();
            ViewBag.lstItemTypes = _itemTypeService.GetAll();
            ViewBag.lstOs = _osService.GetAll();
        }
    }
}
