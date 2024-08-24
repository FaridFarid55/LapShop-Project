// Ignore Spelling: Cls

namespace Bl
{
    public interface Iitems
    {
        public List<TbItem> GetAll();
        public List<VwItem> GetAllItemsData(int? CategoryId);
        public List<VwItem> GetRecommendedItems(int itemId);
        public TbItem GetById(int id);
        public VwItem GetItemById(int id);
        public bool Save(TbItem Items);
        public bool Delete(int Id);
    }
    public class ClsItems : Iitems
    {
        private readonly LapShopContext _Context;

        // constrictor
        public ClsItems(LapShopContext Context)
        {
            _Context = Context;
        }

        // Method
        public List<TbItem> GetAll()
        {
            try
            {
                var ListItems = _Context.TbItems.ToList();
                return ListItems;
            }
            catch
            {
                return new List<TbItem>();
            }
        }

        public List<VwItem> GetAllItemsData(int? CategoryId)
        {
            try
            {
                var ListCategory = _Context.VwItems.Where(i => (i.CategoryId == CategoryId || CategoryId == null || CategoryId == 0)
                && i.CurrentState == 1).OrderByDescending(i => i.CreatedDate).ToList();
                return ListCategory;
            }
            catch
            {
                return new List<VwItem>();
            }
        }

        public List<VwItem> GetRecommendedItems(int itemId)
        {
            try
            {
                // set  details item
                var item = GetById(itemId);

                // check price > 50 and < 50
                var ListCategory = _Context.VwItems.Where(i => i.SalesPrice < item.SalesPrice + 50 && 
                i.SalesPrice > item.SalesPrice -50
                && i.CurrentState == 1).OrderByDescending(i => i.CreatedDate).ToList();
                return ListCategory;
            }
            catch
            {
                return new List<VwItem>();
            }
        }

        public TbItem GetById(int id)
        {
            try
            {
                var item = _Context.TbItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;
            }
            catch
            {
                return new TbItem();
            }
        }
        public VwItem GetItemById(int id)
        {
            try
            {
                var item = _Context.VwItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;
            }
            catch
            {
                return new VwItem();
            }
        }



        public bool Save(TbItem Items)
        {
            try
            {
                Items.CurrentState = 1;
                if (Items.ItemId == 0)
                {
                    Items.CreatedBy = "1";
                    Items.CreatedDate = DateTime.Now;
                    _Context.TbItems.Add(Items);
                }
                else
                {
                    Items.UpdatedBy = "1";
                    Items.UpdatedDate = DateTime.Now;
                    _Context.Entry(Items).State = EntityState.Modified;
                }
                // save
                _Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var Items = GetById(Id);
                Items.CurrentState = 0;
                _Context.Entry(Items).State = EntityState.Modified;
                _Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
