

namespace LapShop_Project.Controllers
{
    public class ItemsController : Controller
    {
        private readonly Iitems oClsItems;
        private readonly IItemImages oClsItemIamges;
        private readonly VmItemDetails VM;

        // Constrictor
        public ItemsController(Iitems oClsItems, IItemImages oClsItemIamges)
        {
            this.oClsItems = oClsItems;
            VM = new VmItemDetails();
            this.oClsItemIamges = oClsItemIamges;
        }

        // Method
        public IActionResult ItemsDetails(int id)
        {
            VM.Item = oClsItems.GetItemById(id);
            VM.ListItemImages = oClsItemIamges.GetByItemId(id);
            VM.ListRecommendedItems = oClsItems.GetRecommendedItems(id).Take(18).ToList();
            return View(VM);
        }
    }
}
