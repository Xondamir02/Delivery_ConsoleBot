using botTest.Models;

namespace botTest.Services.ProductServices
{
    class ProductService
    {
        public List<Product>? Products { get; set; }

        public ProductService()
        {
            Products = new List<Product>();
        }
    }
}
