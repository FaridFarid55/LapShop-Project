


namespace LapShop_Project.Areas.admin.Controllers
{

    [Area("admin")]
    public class SettingsController : Controller
    {
        // Filed
        private readonly ISettings oClsSettings;

        //Constrictor
        public SettingsController(ISettings oClsSettings)
        {
            this.oClsSettings = oClsSettings;
        }
        // Method
        [Authorize(Roles = "Admin,Data Entry,Owner")]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Website()
        {
            var item = oClsSettings.GetById(1);
            var viewModel = new LayoutViewModel
            {
                Copyright = item.Copyright,
                LogoUrl = item.Logo
            };

            ViewData["LayoutViewModel"] = viewModel;

            return View(oClsSettings.GetAll());
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Edit_Website(int Id)
        {
            return View(oClsSettings.GetById(Id));
        }

        [HttpPost]
        public IActionResult Save(TbSetting item, IFormFile uploadedImage)
        {
            if (!ModelState.IsValid)
                return View("Edit_Website", item);

            // save in data base
            item.Logo = uploadedImage.FileName;
            oClsSettings.Save(item);

            return RedirectToAction("Website");
        }
    }
}
