
namespace Domains;

public partial class VwItemsLaptop
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal SalesPrice { get; set; }

    public decimal PurchasePrice { get; set; }

    public int CategoryId { get; set; }

    public string? ImageName { get; set; }

    public string? Description { get; set; }

    public string? Gpu { get; set; }

    public string? HardDisk { get; set; }

    public string CategoryName { get; set; } = null!;
}
