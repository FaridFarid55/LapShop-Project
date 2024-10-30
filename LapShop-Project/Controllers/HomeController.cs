namespace LapShop_Project.Controllers
{
    public class VwItemsCategory
    {
        public string ItemsName { get; set; }
        public string CategoryName { get; set; }
    }

    public class HomeController : Controller
    {
        // Filed
        private readonly Iitems oClsItem;
        private readonly VmHomePage VmPage;
        private readonly ISettings oClsSettings;

        //Constrictor
        public HomeController(Iitems ClsItem, ISettings oClsSettings)
        {
            this.oClsSettings = oClsSettings;
            oClsItem = ClsItem;
            VmPage = new VmHomePage();
        }

        // Method
        public IActionResult Index()
        {
            // show data
            VmPage.ListAllItems = oClsItem.GetAllItemsData(null).Take(24).ToList();
            VmPage.ListRecommendedProductsItems = oClsItem.GetAllItemsData(null).Skip(40).Take(10).ToList();
            VmPage.ListNewItems = oClsItem.GetAllItemsData(null).Skip(60).Take(10).ToList();
            VmPage.ListFreeDelivery = oClsItem.GetAllItemsData(null).Skip(100).Take(6).ToList();
            return View(VmPage);
        }
    }
}
