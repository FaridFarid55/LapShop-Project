using Bl.Classes;

namespace LapShop_Project.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin,Data Entry,Owner")]
    public class HomeController : Controller
    {
        // Field
        private readonly ISettings _settingsService;

        // Constructor
        public HomeController(ISettings settingsService)
        {
            _settingsService = settingsService;
        }

        // Method: Index
        public async Task<IActionResult> Index()
        {
            try
            {
                // Example of fetching settings asynchronously (if applicable in the future)
                // var settings = await _settingsService.GetSettingsAsync();

                return View();
            }
            catch (Exception ex)
            {
                // Provide user-friendly feedback
                ModelState.AddModelError(string.Empty, "An error occurred while loading the page. Please try again later.");
                return View();
            }
        }
    }
}
