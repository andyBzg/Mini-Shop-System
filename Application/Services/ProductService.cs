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

        public List<Product> GetAvailableProducts()
        {
            return _productRepository.LoadAll().Where(p => !p.IsDeleted).ToList();
        }

        public Product? GetProductById(Guid id)
        {
            return _productRepository.GetById(id);
        }

        public bool AddNewProduct(string name, string description, decimal price, int stock)
        {
            Guid id = Guid.NewGuid();
            Product product = new Product(id, name, description, price, stock);
            _productRepository.Save(product);
            return true;
        }

        public bool UpdateProduct(Guid id, string name, string description, decimal price, int stock)
        {
            Product? product = GetProductById(id);
            if (product == null)
                return false;

            if (!string.IsNullOrEmpty(name) && product.Name != name)
                product.Name = name;
            if (!string.IsNullOrEmpty(description) && product.Description != description)
                product.Description = description;
            if (price != product.Price)
                product.Price = price;
            if (stock != product.Stock)
                product.Stock = stock;

            _productRepository.Update(product);
            return true;
        }

        public bool DeleteProduct(Guid id)
        {
            Product? product = _productRepository.GetById(id);
            if (product == null)
                return false;

            product.IsDeleted = true;
            _productRepository.Update(product);
            return true;
        }

        public bool ReduceProductQuantity(Guid id, int quantity)
        {
            Product? product = _productRepository.GetById(id);
            if (product == null)
                return false;

            product.Stock -= quantity;
            _productRepository.Update(product);
            return true;
        }
    }
}
