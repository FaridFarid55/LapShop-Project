namespace Domains;

public partial class TbForm
{
    public int FormId { get; set; }

    public string? FormName { get; set; }

    public string? ControllerName { get; set; }

    public virtual ICollection<TbPermission> TbPermissions { get; set; } = new List<TbPermission>();
}
