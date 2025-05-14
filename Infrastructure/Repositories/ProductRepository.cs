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

        private void WriteJsonToFile(List<Product> products)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            string jsonString = JsonSerializer.Serialize(products, options);

            File.WriteAllText(_filePath, jsonString);
        }

        public List<Product> LoadAll()
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

        public Product? GetById(Guid id)
        {
            List<Product> products = LoadAll();
            return products.FirstOrDefault(p => p.Id == id);
        }

        public void Save(Product product)
        {
            List<Product> products = LoadAll();
            products.Add(product);
            WriteJsonToFile(products);
        }

        public void Update(Product product)
        {
            List<Product> products = LoadAll();
            int index = products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
            {
                products[index] = product;
                WriteJsonToFile(products);
            }
        }
    }
}
