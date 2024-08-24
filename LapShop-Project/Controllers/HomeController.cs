


namespace LapShop_Project.Controllers
{
    public class VwItemsCategory()
    {
        public string ItemsName { get; set; }
        public string CategoryName { get; set; }
    }
    public class HomeController : Controller
    {
        LapShopContext _Context;
        Iitems oClsItem;
        VmHomePage VmPage;
        public HomeController(LapShopContext Context, Iitems ClsItem)
        {
            _Context = Context;
            oClsItem = ClsItem;
            VmPage = new VmHomePage();
        }
        public IActionResult Index()
        {
            // show data
            VmPage.ListAllItems = oClsItem.GetAllItemsData(null).Take(20).ToList();
            VmPage.ListRecommendedProductsItems = oClsItem.GetAllItemsData(null).Skip(40).Take(10).ToList();
            VmPage.ListNewItems = oClsItem.GetAllItemsData(null).Skip(60).Take(10).ToList();
            VmPage.ListFreeDelivery = oClsItem.GetAllItemsData(null).Skip(100).Take(4).ToList();

            return View(VmPage);
        }
    }
}
