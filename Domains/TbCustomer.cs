﻿
namespace Domains;

public partial class TbCustomer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public virtual TbBusinessInfo? TbBusinessInfo { get; set; }

    public virtual ICollection<TbItem> Items { get; set; } = new List<TbItem>();
}