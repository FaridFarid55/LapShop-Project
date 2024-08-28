
namespace LapShop_Project.Areas.admin.Controllers
{
    [Area("admin")]
    public class SettingsController : Controller
    {
        [Authorize(Roles = "Admin,Data Entry,Owner")]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize(Roles = "Owner")]
        public IActionResult Website()
        {
            return View();
        }
    }
}
