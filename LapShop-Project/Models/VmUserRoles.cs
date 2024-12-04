// Ignore Spelling: Vm

namespace LapShop_Project.Models
{
    public class VmUserRoles
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
