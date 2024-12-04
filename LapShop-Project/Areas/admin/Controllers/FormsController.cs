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
                return BadRequest("Role ID cannot be null or empty.");
            }


            var role = await _roleManager.FindByNameAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }


            // جلب الأذونات الخاصة بالدور من خلال استدعاء الإجراء المخزن
            var forms = _context.sss
                .FromSqlRaw("EXEC GetFormsWithPermissions @roleId = {0}", role.Id)
                .ToList();

            return View(nameof(Index), forms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePermissions(IEnumerable<VPermission> permissions)
        {

            // تحديث البيانات في قاعدة البيانات بناءً على القيمة الجديدة
            foreach (var permission in permissions)
            {
                var existingPermission = _context.TbPermissions
                                                 .FirstOrDefault(p => p.PermissionId == permission.PermissionId);
                if (existingPermission != null)
                {
                    existingPermission.HasPermission = permission.HasPermission;
                    // تحديث الحقول الأخرى إذا لزم الأمر
                }
            }

            _context.SaveChanges();


            return Redirect("Websit");
        }

    }
}
