using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetProducts()
        {
            return _productRepository.LoadAllProducts();
        }

        public Product? GetProductById(Guid id)
        {
            return _productRepository.GetProductById(id);
        }
    }
}
