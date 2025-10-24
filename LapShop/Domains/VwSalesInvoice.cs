namespace Domains;

public partial class VwSalesInvoice
{
    public int InvoiceId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? ImageName { get; set; }

    public string ItemName { get; set; } = null!;

    public double Qty { get; set; }

    public decimal InvoicePrice { get; set; }

    public Guid CustomerId { get; set; }

    public double? TotalPrice { get; set; }
}
