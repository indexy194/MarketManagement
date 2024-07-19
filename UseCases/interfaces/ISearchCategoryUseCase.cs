using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.interfaces
{
    public interface ISearchCategoryUseCase
    {
        IEnumerable<Category> Search(string productName);
    }
}
