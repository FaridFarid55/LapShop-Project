using Bl.Classes;

namespace ElectronicsFix.Areas.admin.Controllers
{
    [Authorize(Roles = "Owner")]
    [Area("admin")]
    public class MembersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly LapShopContext _context;

        public MembersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, LapShopContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userRolesViewModel = new List<VmUserRoles>();

                foreach (var user in users)
                {
                    if (user.FirstName == "Owner") continue;

                    var thisViewModel = new VmUserRoles
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        Roles = await _userManager.GetRolesAsync(user)
                    };

                    userRolesViewModel.Add(thisViewModel);
                }

                return View(userRolesViewModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the user list. Please try again.");
                return View(new List<VmUserRoles>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var roles = await _roleManager.Roles.ToListAsync();

                var model = new VmEditUserRoles
                {
                    UserId = user.Id,
                    Email = user.Email,
                    AvailableRoles = roles.Select(r => r.Name).ToList(),
                    SelectedRoles = userRoles.ToList()
                };

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading user roles. Please try again.");
                return View(new VmEditUserRoles());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VmEditUserRoles model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                var currentRoles = await _userManager.GetRolesAsync(user);
                var selectedRoles = model.SelectedRoles ?? new List<string>(); // Ensure it's not null

                // Remove existing roles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to remove existing roles.");
                    return View(model);
                }

                // Add new roles
                var addResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to add new roles.");
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while updating user roles. Please try again.");
                return View(model);
            }
        }
    }
}
