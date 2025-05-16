namespace Application.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer;
        public bool IsDeleted { get; set; } = false;

        public User (Guid id, string username,string email, string passwordHash)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
