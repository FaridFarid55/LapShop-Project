namespace Domains;

public class VPermission
{
    [Key]
    public int FormId { get; set; }
    public string FormName { get; set; }
    public string ControllerName { get; set; }
    public int PermissionId { get; set; }
    public string ActionName { get; set; }
    public bool HasPermission { get; set; }
}
