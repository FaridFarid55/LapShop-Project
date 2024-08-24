namespace LapShop_Project.Models
{
    public class ShoppingCardItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = default!;
        public string ImageName { get; set; } = default!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
