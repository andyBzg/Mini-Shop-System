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

        private void SaveProducts(List<Product> products)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            string jsonString = JsonSerializer.Serialize(products, options);

            File.WriteAllText(_filePath, jsonString);
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

        public void Add(Product product)
        {            
            List<Product> products = LoadAllProducts();
            products.Add(product);
            SaveProducts(products);
        }

        public void Delete(Product product)
        {
            List<Product > products = LoadAllProducts();
            int index = products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
            {
                products[index] = product;
                SaveProducts(products);
            }
        }
    }
}
