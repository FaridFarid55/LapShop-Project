// Ignore Spelling: Cls


namespace Bl
{
    public class ClsOs : ILapShop<TbO>
    {

        private readonly LapShopContext _Context;
        public ClsOs(LapShopContext Context)
        {
            _Context = Context;
        }

        public List<TbO> GetAll()
        {
            try
            {
                var ListOs = _Context.TbOs.Where(o => o.CurrentState == 1).ToList();
                return ListOs;
            }
            catch
            {
                return new List<TbO>();
            }
        }

        public TbO GetById(int Id)
        {
            try
            {
                var Os = _Context.TbOs.FirstOrDefault(o => o.OsId == Id && o.CurrentState == 1);
                return Os;
            }
            catch
            {
                return new TbO();
            }
        }

        public bool Save(TbO Os)
        {
            try
            {
                if (Os.OsId == 0)
                {
                    Os.CreatedBy = "1";
                    Os.CreatedDate = DateTime.Now;
                    _Context.TbOs.Add(Os);
                }
                else
                {
                    Os.UpdatedBy = "1";
                    Os.UpdatedDate = DateTime.Now;
                    _Context.Entry(Os).State = EntityState.Modified;
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
                var Os = GetById(Id);
                _Context.TbOs.Remove(Os);
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
