using Application.Interfaces;
using Application.Models;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _filePath;

        public OrderRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Order> LoadAll()
        {
            if (!File.Exists(_filePath))
                return new List<Order>();

            string jsonString = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            List<Order>? orders = JsonSerializer.Deserialize<List<Order>>(jsonString, options);

            return orders ?? new List<Order>();
        }

        public Order? GetById(Guid id)
        {
            List<Order> orders = LoadAll();
            return orders.FirstOrDefault(o => o.Id == id);
        }

        public void Save(Order order)
        {
            List<Order> orders = LoadAll();
            orders.Add(order);
            WriteJsonToFile(orders);
        }

        public void Update(Order order)
        {
            List<Order> orders = LoadAll();
            int index = orders.FindIndex(o => o.Id == order.Id);
            if (index != -1)
            {
                orders[index] = order;
                WriteJsonToFile(orders);
            }
        }

        private void WriteJsonToFile(List<Order> orders)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            string jsonString = JsonSerializer.Serialize(orders, options);

            File.WriteAllText(_filePath, jsonString);
        }
    }
}
