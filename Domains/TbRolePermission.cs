namespace Domains;

public partial class TbRolePermission
{
    public int RolePermissionId { get; set; }

    public string RoleId { get; set; } = null!;

    public int PermissionId { get; set; }

    public virtual TbPermission Permission { get; set; } = null!;

    public virtual AspNetRole Role { get; set; } = null!;
}
