
namespace LapShop_Project.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin,Data Entry")]
    [Area("admin")]
    public class ItemsController : Controller
    {
        private List<VwItem> items;
        private readonly Iitems oClsItems;
        private readonly ILapShop<TbCategory> oClsCategories;
        private readonly ILapShop<TbO> oClsOs;
        private readonly ILapShop<TbItemType> oClsItemTypes;

        // Consistent
        public ItemsController(Iitems Item, ILapShop<TbCategory> Category,
            ILapShop<TbO> Os, ILapShop<TbItemType> ItemType)
        {
            oClsItems = Item;
            oClsCategories = Category;
            oClsItemTypes = ItemType;
            oClsOs = Os;
            items = new List<VwItem>();
        }

        [HttpGet]
        public IActionResult List(int? itemId)
        {
            ViewBag.ListCategorie = oClsCategories.GetAll();
            if (itemId != null)
                items = oClsItems.GetAllItemsData(itemId);
            else
                items = oClsItems.GetAllItemsData(null);
            return View(items);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int? ItemId)
        {
            TbItem item = new TbItem();
            ViewBag.listCategories = oClsCategories.GetAll();
            ViewBag.lstItemTypes = oClsItemTypes.GetAll();
            ViewBag.lstOs = oClsOs.GetAll();
            if (ItemId != null)
                item = oClsItems.GetById(Convert.ToInt32(ItemId));

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem Item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.listCategories = oClsCategories.GetAll();
                ViewBag.lstItemTypes = oClsItemTypes.GetAll();
                ViewBag.lstOs = oClsOs.GetAll();
                return View(nameof(Edit), Item);
            }


            Item.ImageName = await ClsUiHelper.UploadImage(Files, "Items");
            oClsItems.Save(Item);

            return RedirectToAction(nameof(List));

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int itemId)
        {
            oClsItems.Delete(itemId);

            return RedirectToAction("list");
        }

        public IActionResult Search(int Id)
        {
            ViewBag.ListCategorie = oClsCategories.GetAll();
            return View("List", oClsItems.GetAllItemsData(Id));
        }
    }
}
