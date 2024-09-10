
namespace LapShop_Project.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin,Data Entry,Owner")]
    public class HomeController : Controller
    {
        // Filed
        private readonly ISettings oClsSettings;

        //Constrictor
        public HomeController(ISettings oClsSettings)
        {
            this.oClsSettings = oClsSettings;
        }

        // Method
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
