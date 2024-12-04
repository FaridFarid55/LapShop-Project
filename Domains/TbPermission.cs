namespace Domains;

public partial class TbPermission
{
    public int PermissionId { get; set; }

    public string? ActionName { get; set; }

    public string? PermissionName { get; set; }

    public bool HasPermission { get; set; }

    public int FormId { get; set; }

    public virtual TbForm Form { get; set; } = null!;

    public virtual ICollection<TbRolePermission> TbRolePermissions { get; set; } = new List<TbRolePermission>();
}
