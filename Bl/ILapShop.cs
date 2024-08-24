
namespace Bl
{
    public interface ILapShop<T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int id);
        public bool Save(T Items);
        public bool Delete(int Id);
    }
}
