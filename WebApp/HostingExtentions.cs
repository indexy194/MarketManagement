using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.interfaces;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;
using UseCases;
using CoreBusiness;


namespace WebApp
{
    public static class HostingExtentions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddDbContext<MarketContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<MarketContext>();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Inventory", p => p.RequireClaim("Position", "Inventory"));
                options.AddPolicy("Cashiers", p => p.RequireClaim("Position", "Cashier"));
            });

            if (builder.Environment.IsEnvironment("QA"))
            {
                //test case
                builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
                builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
                builder.Services.AddSingleton<ITransactionRepository, TransactionsInMemoryRepository>();
            }
            else
            {
                builder.Services.AddTransient<ICategoryRepository, CategorySQLRepository>();
                builder.Services.AddTransient<IProductRepository, ProductSQLRepository>();
                builder.Services.AddTransient<ITransactionRepository, TransactionSQLRepository>();
            }


            builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
            builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCase>();
            builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
            builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
            builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
            builder.Services.AddTransient<ISearchCategoryUseCase, SearchCategoryUseCase>();

            builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
            builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
            builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
            builder.Services.AddTransient<IViewProductsInCategoryUseCase, ViewProductsInCategoryUseCase>();
            builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
            builder.Services.AddTransient<IViewSelectedProductUseCase, ViewSelectedProductUseCase>();
            builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();
            builder.Services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();

            builder.Services.AddTransient<IRecordTransactionUseCase, RecordTransactionUseCase>();
            builder.Services.AddTransient<IGetTodayTransactionsUseCase, GetTodayTransactionsUseCase>();
            builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();
            builder.Services.AddTransient<IListAllTransactionUseCase, ListAllTransactionUseCase>();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            return app;
        }
    }
}
