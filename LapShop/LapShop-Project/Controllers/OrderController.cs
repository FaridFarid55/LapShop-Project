using Bl.Classes;

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
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var ListInvoice = oClsSalesInvoice.GetById(Guid.Parse(user.Id));
            ViewBag.Invoice = ListInvoice[0];

            return View(nameof(OrderSuccess), ListInvoice);
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

            // remove  cookies
            HttpContext.Response.Cookies.Delete("Cart");

            // add id user 
            var user = await _userManager.GetUserAsync(User);
            var ListInvoice = oClsSalesInvoice.GetById(Guid.Parse(user.Id));
            ViewBag.Invoice = ListInvoice[0];

            return View(ListInvoice);
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

        // logic
        private void SetCart(ShoppingCard cart)
        {
            var cartCookieValue = JsonConvert.SerializeObject(cart);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // صلاحية الكوكي
                HttpOnly = true
            };
            HttpContext.Response.Cookies.Append("Cart", cartCookieValue, options);
        }
        private ShoppingCard GetCart()
        {
            var cartCookie = HttpContext.Request.Cookies["Cart"];

            if (!string.IsNullOrEmpty(cartCookie))
            {
                return JsonConvert.DeserializeObject<ShoppingCard>(cartCookie);
            }
            return new ShoppingCard(); // إنشاء سلة جديدة إذا كانت فارغة
        }
        public IActionResult RemoveFromCart(int productId)
        {
            // استرجاع سلة التسوق من الكوكي
            var cart = GetCart();

            // حذف العنصر الذي يحمل ItemId المطابق
            cart.ListItems = cart.ListItems.Where(item => item.ItemId != productId).ToList();

            // إعادة حساب المجموع الإجمالي بعد الحذف
            cart.Total = cart.ListItems.Sum(item => item.Total);


            // إعادة تحديث الكوكي بسلة التسوق المحدثة
            SetCart(cart);

            return RedirectToAction("Cart"); // إعادة التوجيه إلى صفحة السلة
        }
    }
}
