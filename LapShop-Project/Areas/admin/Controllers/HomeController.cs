



namespace LapShop_Project.Areas.admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin,Data Entry,Owner")]
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }
    }
}
