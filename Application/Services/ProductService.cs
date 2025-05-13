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
            return _productRepository.LoadAllProducts().Where(p => !p.IsDeleted).ToList();
        }

        public Product? GetProductById(Guid id)
        {
            return _productRepository.GetProductById(id);
        }

        public void AddNewProduct(string name, string description, decimal price, int stock)
        {
            Guid id = Guid.NewGuid();
            Product product = new Product(id, name, description, price, stock);
            _productRepository.Add(product);
        }

        public bool DeleteProduct(Guid id)
        {
            Product? product = _productRepository.GetProductById(id);
            if (product == null) 
                return false; 

            product.IsDeleted = true;
            _productRepository.Delete(product);
            return true;
        }
    }
}
