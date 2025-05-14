using Application.Interfaces;
using Application.Models;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath;

        public UserRepository(string filePath)
        {
            _filePath = filePath;
        }

        private Dictionary<Guid, User> LoadUsers()
        {
            if (!File.Exists(_filePath))
                return new Dictionary<Guid, User>();

            string jsonString = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            Dictionary<Guid, User>? users = JsonSerializer.Deserialize<Dictionary<Guid, User>>(jsonString, options);

            return users ?? new Dictionary<Guid, User>();
        }

        private void SaveUsers(Dictionary<Guid, User> users)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            string jsonString = JsonSerializer.Serialize(users, options);

            File.WriteAllText(_filePath, jsonString);
        }

        public Dictionary<Guid, User> GetAll()
        {
            return LoadUsers();
        }

        public void Add(Guid guid, User user)
        {
            Dictionary<Guid, User> users = LoadUsers();

            if (!users.ContainsKey(guid))
            {
                users.Add(guid, user);
                SaveUsers(users);
            }
        }

        public User? GetUserByEmail(string email)
        {
            Dictionary<Guid, User> users = LoadUsers();
            return users.Values.FirstOrDefault(u => u.Email == email);
        }
    }
}
