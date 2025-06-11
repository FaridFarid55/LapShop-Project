using Bl.Classes;

namespace LapShop_Project.Areas.admin.Controllers
{
    [Area("admin")]
    public class FormsController : Controller
    {
        private readonly LapShopContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FormsController(LapShopContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> ManagePermissions(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                ModelState.AddModelError(string.Empty, "Role ID cannot be null or empty.");
                return View(nameof(Index), Enumerable.Empty<VPermission>());
            }

            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ModelState.AddModelError(string.Empty, "Role not found.");
                    return View(nameof(Index), Enumerable.Empty<VPermission>());
                }

                var forms = _context.VPermissions
                    .FromSqlRaw("EXEC GetFormsWithPermissions @roleId = {0}", role.Id)
                    .ToList();

                return View(nameof(Index), forms);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching permissions.");
                return View(nameof(Index), Enumerable.Empty<VPermission>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePermissions(IEnumerable<VPermission> permissions)
        {
            if (permissions == null || !permissions.Any())
            {
                ModelState.AddModelError(string.Empty, "No permissions provided to update.");
                return RedirectToAction(nameof(ManagePermissions));
            }

            try
            {
                foreach (var permission in permissions)
                {
                    var existingPermission = await _context.TbPermissions
                        .FirstOrDefaultAsync(p => p.PermissionId == permission.PermissionId);

                    if (existingPermission != null)
                    {
                        existingPermission.HasPermission = permission.HasPermission;
                        // Update additional fields if necessary
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Adjust to the appropriate action or view
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving permissions.");
                return RedirectToAction(nameof(ManagePermissions));
            }
        }
    }
}
