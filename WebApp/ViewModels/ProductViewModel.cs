using CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }   
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public Product Product { get; set; } = new Product();
    }
}
