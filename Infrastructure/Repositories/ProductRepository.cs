using Application.Interfaces;
using Application.Models;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _filePath;

        public ProductRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Product> LoadAllProducts()
        {
            if (!File.Exists(_filePath))
                return new List<Product>();

            string jsonString = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(jsonString, options);

            return products ?? new List<Product>();
        }

        public Product? GetProductById(Guid id)
        {
            List<Product> products = LoadAllProducts();
            return products.FirstOrDefault(p => p.Id == id);
        }
    }
}
