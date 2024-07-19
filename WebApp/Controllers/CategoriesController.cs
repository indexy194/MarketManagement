using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using UseCases.interfaces;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize(Policy = "Inventory")]
    public class CategoriesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCase;
        private readonly IEditCategoryUseCase editCategoryUseCase;
        private readonly IAddCategoryUseCase addCategoryUseCase;
        private readonly IDeleteCategoryUseCase deleteCategoryUseCase;
        private readonly ISearchCategoryUseCase searchCategoryUseCase;

        public CategoriesController(
            UserManager<IdentityUser> userManager,
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
            IEditCategoryUseCase editCategoryUseCase,
            IAddCategoryUseCase addCategoryUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase,
            ISearchCategoryUseCase searchCategoryUseCase)
        {
            this.userManager = userManager;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
            this.editCategoryUseCase = editCategoryUseCase;
            this.addCategoryUseCase = addCategoryUseCase;
            this.deleteCategoryUseCase = deleteCategoryUseCase;
            this.searchCategoryUseCase = searchCategoryUseCase;
        }

        public IActionResult Index()
        {

            var categories = new CategoryViewModel
            {
                Categories = viewCategoriesUseCase.Execute()
            };
            return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            var category = viewSelectedCategoryUseCase.Execute(id.HasValue ? id.Value : 0);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                editCategoryUseCase.Execute(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            return View(category);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }
        public IActionResult Search(CategoryViewModel categoryViewModel)
        {
            var result = new CategoryViewModel
            {
                Categories = searchCategoryUseCase.Search(categoryViewModel.CateName??string.Empty)
            };
            return View("Index", result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            var entry = await userManager.GetUserAsync(User);
            if (entry == null)
            {
                return View(category);
            }
            if (ModelState.IsValid)
            {
                category.UserId = entry.Id;
                addCategoryUseCase.Execute(category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            return View(category);
        }

        public IActionResult Delete(int categoryId)
        {
            deleteCategoryUseCase.Execute(categoryId);
            return RedirectToAction(nameof(Index));
        }

    }
}
