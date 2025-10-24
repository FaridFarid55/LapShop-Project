using Bl.Classes;

namespace LapShop_Project.Areas.admin.Controllers
{
    [Area("admin")]
    public class SettingsController : Controller
    {
        private readonly ISettings _clsSettings;

        // Constructor
        public SettingsController(ISettings clsSettings)
        {
            _clsSettings = clsSettings;
        }

        // GET: Profile (Restricted to Admin, Data Entry, Owner)
        [Authorize(Roles = "Admin,Data Entry,Owner")]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        // GET: Website (Restricted to Owner)
        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Website()
        {
            var item = _clsSettings.GetById(1);
            var viewModel = new VmLayout
            {
                Copyright = item.Copyright,
                LogoUrl = item.Logo
            };

            ViewData["LayoutViewModel"] = viewModel;

            return View(_clsSettings.GetAll());
        }

        // GET: Edit Website (Restricted to Owner)
        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Edit_Website(int id)
        {
            var item = _clsSettings.GetById(id);
            if (item == null)
            {
                ModelState.AddModelError("", "Website settings not found.");
                return RedirectToAction("Website");
            }
            return View(item);
        }

        // POST: Save Website Settings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbSetting item, IFormFile uploadedImage)
        {
            try
            {
                // Upload image file if provided
                if (uploadedImage != null)
                {
                    item.Logo = await ClsUiHelper.UploadImage(uploadedImage, "/FrontEnd/images/Logo");
                    ModelState.Remove("Logo");
                }
                else ModelState.Remove("uploadedImage");



                if (!ModelState.IsValid) return View("Edit_Website", item);


                // save in data base

                if (uploadedImage != null) item.Logo = uploadedImage.FileName;
                _clsSettings.Save(item);

                return RedirectToAction("Website");
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                ModelState.AddModelError(string.Empty, "An error occurred while saving the website settings.");
                return View("Edit_Website", item);
            }
        }
    }
}
