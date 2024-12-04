namespace LapShop_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly Iitems ItemService;
        private ShoppingCard cart;
        private UserManager<ApplicationUser> _userManager;
        private ISalesInvoice oClsSalesInvoice;

        // Constrictor
        public OrderController(Iitems ItemService, UserManager<ApplicationUser> userManager, ISalesInvoice oClsSalesInvoice)
        {
            this.ItemService = ItemService;
            cart = new ShoppingCard();
            _userManager = userManager;
            this.oClsSalesInvoice = oClsSalesInvoice;
        }

        private List<ShoppingCard> GetCartItems()
        {
            var cartCookie = Request.Cookies["Cart"];

            if (!string.IsNullOrEmpty(cartCookie))
                return JsonConvert.DeserializeObject<List<ShoppingCard>>(cartCookie);
            return new List<ShoppingCard>();
        }
        private void SetCartItems(List<ShoppingCard> cartItems)
        {
            var cartJson = JsonConvert.SerializeObject(cartItems);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                Path = "/"
            };
            Response.Cookies.Append("Cart", cartJson, options);
        }


        // Method 
        [HttpGet]
        public IActionResult Cart()
        {
            string sSessionCart = string.Empty;
            ShoppingCard? card = new ShoppingCard();
            // get session && check session null
            if (HttpContext.Request.Cookies["Cart"] != null)
            {
                sSessionCart = HttpContext.Request.Cookies["Cart"];

                card = JsonConvert.DeserializeObject<ShoppingCard>(sSessionCart);
            }


            return View(card);
        }

        public IActionResult AddToCart(int itemId)
        {

            //check session null
            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCard>(HttpContext.Request.Cookies["Cart"]);

            var item = ItemService.GetById(itemId);

            var itemInList = cart.ListItems.Where(c => c.ItemId == itemId).FirstOrDefault();

            // check listItem == null
            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.Total = itemInList.Qty * itemInList.Price;
            }
            else
            {
                // set data in list item
                cart.ListItems.Add(new ShoppingCardItem()
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ImageName = item.ImageName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice
                });
            }

            // sum total price 
            cart.Total = cart.ListItems.Sum(c => c.Total);


            // set Session
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            //HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction(nameof(Cart));
        }


        [HttpGet]
        public IActionResult MyOrders()
        {
            return View();
        }


        public IActionResult RemoveFromCart(decimal productName)
        {
            // استرجاع المنتجات المخزنة في سلة التسوق
            var cartItems = GetCartItems();

            // حذف المنتج الذي يحمل المعرف المحدد
            var updatedCartItems = cartItems.Where(item => item.Total != productName).ToList();

            // تحديث الكوكي بعد الحذف
            SetCartItems(updatedCartItems);

            return RedirectToAction("Cart"); // إعادة التوجيه إلى صفحة سلة التسوق
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> OrderSuccess(string myCard)
        {

            var myCardList = new ShoppingCard();
            string sSessionCart = string.Empty;

            if (HttpContext.Request.Cookies["Cart"] != null)
                sSessionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCard>(sSessionCart);

            // check as cart 
            if (cart == null)
            {
                TempData["ErrorMessage"] = "Shopping cart is empty.";
                return Redirect("/Order/Cart");
            }
            // set  ShoppingCard
            if (!string.IsNullOrEmpty(myCard)) myCardList = JsonConvert.DeserializeObject<ShoppingCard>(myCard);
            else
            {
                TempData["ErrorMessage"] = "Not Found.";
                return Redirect("/Order/Cart");
            }
            // save order
            await SaveOrder(cart);

            return View(myCardList);
        }

        public async Task SaveOrder(ShoppingCard oShoppingCard)
        {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();

                //
                foreach (var item in oShoppingCard.ListItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem()
                    {
                        ItemId = item.ItemId,
                        Qty = item.Qty,
                        InvoicePrice = item.Price
                    });
                }

                var user = await _userManager.GetUserAsync(User);

                //
                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.UserName,
                    CreatedDate = DateTime.Now
                };

                oClsSalesInvoice.Save(oSalesInvoice, lstInvoiceItems, true, user.UserName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
