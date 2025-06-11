// Ignore Spelling: Cls

namespace Bl.Classes;

public class ClsCategories : ILapShop<TbCategory>
{

    private readonly LapShopContext _Context;
    public ClsCategories(LapShopContext Context)
    {
        _Context = Context;
    }

    public List<TbCategory> GetAll()
    {
        try
        {
            var ListCategories = _Context.TbCategories.Where(c => c.CurrentState == 1).ToList();
            return ListCategories;
        }
        catch
        {
            return new List<TbCategory>();
        }
    }

    public TbCategory GetById(int Id)
    {
        try
        {
            var Category = _Context.TbCategories.FirstOrDefault(c => c.CategoryId == Id && c.CurrentState == 1);
            return Category;
        }
        catch
        {
            return new TbCategory();
        }
    }

    public bool Save(TbCategory category)
    {
        try
        {
            category.CurrentState = 1;
            if (category.CategoryId == 0)
            {
                category.CreatedBy = "1";
                category.CreatedDate = DateTime.Now;
                _Context.TbCategories.Add(category);
            }
            else
            {
                category.UpdatedBy = "1";
                category.UpdatedDate = DateTime.Now;
                _Context.Entry(category).State = EntityState.Modified;
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
            var category = GetById(Id);
            category.CurrentState = 0;
            _Context.Entry(category).State = EntityState.Modified;
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
