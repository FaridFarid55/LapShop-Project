// Ignore Spelling: Vm

namespace LapShop_Project.Models
{
    public class VmHomePage
    {
        // Constrictor
        public VmHomePage()
        {
            ListAllItems = new List<VwItem>();
            ListRecommendedProductsItems = new List<VwItem>();
            ListBaner = new List<VwItem>();
            ListDashBorad = new List<VwItem>();
            ListNewItems = new List<VwItem>();
            ListFreeDelivery = new List<VwItem>();
            ListCategories = new List<TbCategory>();
            ListShoppingCard = new List<ShoppingCard>();
        }

        // Property
        public List<VwItem> ListAllItems { get; set; }
        public List<VwItem> ListDashBorad { get; set; }
        public List<VwItem> ListBaner { get; set; }
        public List<VwItem> ListRecommendedProductsItems { get; set; }
        public List<VwItem> ListNewItems { get; set; }
        public List<VwItem> ListFreeDelivery { get; set; }
        public List<TbCategory> ListCategories { get; set; }
        public List<ShoppingCard> ListShoppingCard { get; set; }
    }
}
