using Microsoft.EntityFrameworkCore;

namespace Bl.Classes
{
    public interface IItemImages
    {
        public List<TbItemImage> GetByItemId(int itemId);
    }
    public class ClsItemImages : IItemImages
    {
        private readonly LapShopContext _Context;

        // constrictor
        public ClsItemImages(LapShopContext Context)
        {
            _Context = Context;
        }

        // Method
        public List<TbItemImage> GetByItemId(int id)
        {
            try
            {
                var ListItemIamges = _Context.TbItemImages.Where(g => g.ItemId == id).ToList();
                return ListItemIamges;
            }
            catch
            {
                return new List<TbItemImage>();
            }
        }

    }
}
