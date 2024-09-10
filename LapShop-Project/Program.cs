// Ignore Spelling: App


using Microsoft.AspNetCore.Authentication.Cookies;

namespace LapShop_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<LapShopContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection")));

            // add object
            builder.Services.AddScoped<ILapShop<TbCategory>, ClsCategories>();
            builder.Services.AddScoped<Iitems, ClsItems>();
            builder.Services.AddScoped<ILapShop<TbItemType>, ClsItemTypes>();
            builder.Services.AddScoped<ILapShop<TbO>, ClsOs>();
            builder.Services.AddScoped<IItemImages, ClsItemImages>();
            builder.Services.AddScoped<ISalesInvoice, ClsSalesInvoice>();
            builder.Services.AddScoped<ISalesInvoiceItems, ClsSalesInvoiceItems>();
            builder.Services.AddScoped<ISettings, ClsSettings>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<LayoutDataFilter>();
            });

            // add session
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache();

            // Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LapShopContext>();


            //  Configure app like identity
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/User/AccessDenied";
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = "/User/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });



            var app = builder.Build();

            // check Environment IsDevelopment
            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseHsts();
            }
            //
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();


            app.MapControllerRoute(
                                 name: "admin",
                                 pattern: "{Area:exists}/{Controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                                 name: "LandingPages",
                                 pattern: "{Area:exists}/{Controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                                 name: "default",
                                 pattern: "{Controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
