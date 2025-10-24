// Ignore Spelling: Vm

namespace LapShop_Project.Models
{
    public class VmEditUserRoles
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> AvailableRoles { get; set; } = new List<string>();
        public List<string> SelectedRoles { get; set; } = new List<string>();
    }
}
