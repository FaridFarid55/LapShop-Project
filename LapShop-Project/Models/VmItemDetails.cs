namespace LapShop_Project.Models
{
    public class VmItemDetails
    {
        public VmItemDetails()
        {
            Item = new VwItem();
            ListItemImages = new List<TbItemImage>();
            ListRecommendedItems = new List<VwItem>();
        }

        public VwItem Item { get; set; }
        public List<TbItemImage> ListItemImages { get; set; }
        public List<VwItem> ListRecommendedItems { get; set; }

    }
}
