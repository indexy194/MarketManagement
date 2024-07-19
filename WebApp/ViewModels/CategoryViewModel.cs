using CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CategoryViewModel
    {
        [Display(Name = "Category Name")]
        public string CateName { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public Category Category { get; set; } = new Category();
    }
}
