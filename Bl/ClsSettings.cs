



// Ignore Spelling: Cls

namespace Bl
{
    public interface ISettings
    {
        public List<TbSetting> GetAll();
        public bool Save(TbSetting item);
        public TbSetting GetById(int Id);
    }
    // Form Farid Farid
    public class ClsSettings : ISettings
    {
        // Filed
        private readonly LapShopContext _Context;

        // constrictor
        public ClsSettings(LapShopContext Context)
        {
            _Context = Context;
        }
        public List<TbSetting> GetAll()
        {
            try
            {
                var ListSettigns = _Context.TbSettings.ToList();
                return ListSettigns;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public TbSetting GetById(int Id)
        {
            try
            {
                var item = _Context.TbSettings.FirstOrDefault(a => a.ID == Id);
                return item;
            }
            catch (Exception)
            {
                return new TbSetting();
            }
        }
        public bool Save(TbSetting item)
        {
            try
            {
                var existingSetting = _Context.TbSettings.Local.FirstOrDefault(s => s.ID == item.ID);

                if (existingSetting != null)
                {
                    _Context.Entry(existingSetting).State = EntityState.Detached;
                }

                _Context.Entry(item).State = EntityState.Modified;
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
