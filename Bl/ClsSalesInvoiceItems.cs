
namespace Bl
{
    // Form Farid Farid
    public interface ISalesInvoiceItems
    {
        public List<TbSalesInvoiceItem> GetSalesInvoiceId(int id);
        public bool Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew, string name);
    }
    public class ClsSalesInvoiceItems : ISalesInvoiceItems
    {
        // Fields
        private readonly LapShopContext _Context;

        // Constrictor
        public ClsSalesInvoiceItems(LapShopContext context)
        {

            _Context = context;

        }


        // Method
        public List<TbSalesInvoiceItem> GetSalesInvoiceId(int id)
        {
            try
            {
                var Items = _Context.TbSalesInvoiceItems.Where(a => a.InvoiceId == id).ToList();
                if (Items == null)
                    return new List<TbSalesInvoiceItem>();
                else
                    return Items;
            }
            catch (Exception)
            {
                throw new NotImplementedException(); ;
            }
        }

        public bool Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew, string name)
        {
            List<TbSalesInvoiceItem> dbInvoiceItems = GetSalesInvoiceId(Items[0].InvoiceId);

            foreach (var interfaceItems in Items)
            {
                var dbObject = dbInvoiceItems.FirstOrDefault(a => a.InvoiceItemId == interfaceItems.InvoiceItemId);
                if (dbObject != null)
                    _Context.Entry(dbObject).State = EntityState.Modified;
                else
                {
                    interfaceItems.InvoiceId = salesInvoiceId;
                    _Context.TbSalesInvoiceItems.Add(interfaceItems);
                }
            }

            foreach (var item in dbInvoiceItems)
            {
                var interfaceObject = Items.FirstOrDefault(a => a.InvoiceItemId == item.InvoiceItemId);
                if (interfaceObject == null)
                    _Context.TbSalesInvoiceItems.Remove(item);
            }

            _Context.SaveChanges();
            return true;

        }
    }
}
