// Ignore Spelling: Cls

namespace Bl.Classes
{
    public class ClsItemTypes : ILapShop<TbItemType>
    {

        private readonly LapShopContext _Context;
        public ClsItemTypes(LapShopContext Context)
        {
            _Context = Context;
        }

        public List<TbItemType> GetAll()
        {
            try
            {
                var ListItemType = _Context.TbItemTypes.Where(t => t.CurrentState == 1).ToList();
                return ListItemType;
            }
            catch
            {
                return new List<TbItemType>();
            }
        }

        public TbItemType GetById(int Id)
        {
            try
            {
                var ItemType = _Context.TbItemTypes.FirstOrDefault(t => t.ItemTypeId == Id & t.CurrentState == 1);
                return ItemType;
            }
            catch
            {
                return new TbItemType();
            }
        }

        public bool Save(TbItemType ItemType)
        {
            try
            {
                if (ItemType.ItemTypeId == 0)
                {
                    ItemType.CreatedBy = "1";
                    ItemType.CreatedDate = DateTime.Now;
                    _Context.TbItemTypes.Add(ItemType);
                }
                else
                {
                    ItemType.UpdatedBy = "1";
                    ItemType.UpdatedDate = DateTime.Now;
                    _Context.Entry(ItemType).State = EntityState.Modified;
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
                var ItemType = GetById(Id);
                _Context.TbItemTypes.Remove(ItemType);
                _Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<VwItem> GetAllItemsData(int? CategoryId)
        {
            throw new NotImplementedException();
        }

        public VwItem GetItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
