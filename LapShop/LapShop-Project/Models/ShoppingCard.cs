// Ignore Spelling: Pormocode

namespace LapShop_Project.Models
{
    public class ShoppingCard
    {
        public ShoppingCard()
        {
            ListItems = new List<ShoppingCardItem>();
        }
        public List<ShoppingCardItem> ListItems { get; set; }
        public decimal Total { get; set; }
        public string Pormocode { get; set; } = default!;
    }
}
