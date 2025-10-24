namespace Domains;

public partial class AspNetRole
{
    [Key]
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<TbRolePermission> TbRolePermissions { get; set; } = new List<TbRolePermission>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
