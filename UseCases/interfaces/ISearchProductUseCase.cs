using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.interfaces
{
    public interface ISearchProductUseCase
    {
        IEnumerable<Product> Search(string productName, bool loadCate = false);
    }
}
