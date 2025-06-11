namespace Bl.Classes
{
    // Form Farid Farid
    public interface ISalesInvoice
    {
        public List<VwSalesInvoice> GetAll();
        public TbSalesInvoice GetById(int id);
        public List<VwSalesInvoice> GetById(Guid userId);
        public bool Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew, string name);
        public bool Delete(int Id);
    }
    public class ClsSalesInvoice : ISalesInvoice
    {
        // Fields
        private readonly LapShopContext _Context;
        private readonly ISalesInvoiceItems oClsSalesInvoiceItems;

        // Constrictor
        public ClsSalesInvoice(LapShopContext context, ISalesInvoiceItems oClsSalesInvoiceItems)
        {

            _Context = context;
            this.oClsSalesInvoiceItems = oClsSalesInvoiceItems;
        }

        // Method
        public List<VwSalesInvoice> GetAll()
        {
            try
            {
                return _Context.VwSalesInvoices.ToList();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public TbSalesInvoice GetById(int id)
        {
            try
            {
                var salesInvoice = _Context.TbSalesInvoices.FirstOrDefault(a => a.InvoiceId == id);
                if (salesInvoice == null)
                    return new TbSalesInvoice();
                else
                    return salesInvoice;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public List<VwSalesInvoice> GetById(Guid userId)
        {
            try
            {
                var ShowInvoice = _Context.VwSalesInvoices.Where(a => a.CustomerId == userId).ToList();
                if (ShowInvoice == null)
                    return new List<VwSalesInvoice>();
                else
                    return ShowInvoice;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public bool Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew, string name)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    Item.CurrentState = 1;
                    if (isNew)
                    {
                        Item.CreatedBy = name;
                        Item.CreatedDate = DateTime.Now;
                        _Context.TbSalesInvoices.Add(Item);
                    }

                    else
                    {
                        Item.UpdatedBy = name;
                        Item.UpdatedDate = DateTime.Now;
                        _Context.Entry(Item).State = EntityState.Modified;
                    }

                    _Context.SaveChanges();
                    oClsSalesInvoiceItems.Save(lstItems, Item.InvoiceId, true, name);

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    new NotImplementedException();
                    return false;
                }
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
