using Microsoft.AspNetCore.Mvc;
using CoreBusiness;
using WebApp.ViewModels;
using UseCases.CategoriesUseCases;
using UseCases;
using Microsoft.AspNetCore.Authorization;
using UseCases.ProductsUseCases;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize(Policy = "Cashiers")]
    public class SalesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
        private readonly ISellProductUseCase sellProductUseCase;
        private readonly IViewProductsInCategoryUseCase viewProductsInCategoryUseCase;

        public SalesController(
            UserManager<IdentityUser> userManager,
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase,
            ISellProductUseCase sellProductUseCase,
            IViewProductsInCategoryUseCase viewProductsInCategoryUseCase)
        {
            this.userManager = userManager;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
            this.sellProductUseCase = sellProductUseCase;
            this.viewProductsInCategoryUseCase = viewProductsInCategoryUseCase;

        }

        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = viewCategoriesUseCase.Execute()              
            };
            return View(salesViewModel);
        }

        public IActionResult SellProductPartial(int productId)
        {
            var product = viewSelectedProductUseCase.Execute(productId);
            return PartialView("_SellProduct", product);
        }

        public async Task<IActionResult> Sell(SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                var entry = await userManager.GetUserAsync(User);
                if(entry != null)
                {
                // Sell the product
                    var cashierName = User?.Identity?.Name;
                    sellProductUseCase.Execute(
                        cashierName,
                        userId: entry.Id,
                        salesViewModel.SelectedProductId,
                        salesViewModel.QuantityToSell);
                }
                               
            }

            var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
            salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
            salesViewModel.Categories = viewCategoriesUseCase.Execute();
            
            return View("Index", salesViewModel);
        }

        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            var products = viewProductsInCategoryUseCase.Execute(categoryId);

            return PartialView("_Products", products);
        }
    }
}
