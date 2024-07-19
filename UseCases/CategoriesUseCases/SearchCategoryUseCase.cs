using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;
using UseCases.interfaces;

namespace UseCases.CategoriesUseCases
{
    public class SearchCategoryUseCase : ISearchCategoryUseCase
    {
        private readonly ICategoryRepository categoryRepository;

        public SearchCategoryUseCase(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IEnumerable<Category> Search(string productName)
        {
            return categoryRepository.Search(productName);
        }
    }
}
