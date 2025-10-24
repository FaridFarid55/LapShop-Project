// Ignore Spelling: Cls

namespace Bl.Classes
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
                if (ListItems == null)
                    throw new ArgumentNullException("Not Date");
                return ListItems;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<VwItem> GetAllItemsData(int? categoryId)
        {
            if(categoryId==0) throw new KeyNotFoundException($"Item with ID {categoryId} not found.");
            try
            {
                var ListCategory = _Context.VwItems.Where(i => (categoryId == null || categoryId == 0 || i.CategoryId == categoryId)
                && i.CurrentState == 1).OrderByDescending(i => i.CreatedDate).ToList();



                if (ListCategory == null || ListCategory.Count == 0) throw new KeyNotFoundException($"Item with ID {categoryId} not found.");

                return ListCategory;
            }
            catch (Exception ex)
            {
                throw;
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
                i.SalesPrice > item.SalesPrice - 50
                && i.CurrentState == 1).OrderByDescending(i => i.CreatedDate).ToList();
                return ListCategory;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TbItem GetById(int id)
        {
            try
            {
                var item = _Context.TbItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                if (item == null) throw new KeyNotFoundException($"Item with ID {id} not found.");
                return item;
            }
            catch (Exception ex)
            {
                throw;
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
            catch (Exception ex)
            {
                throw;
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
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
