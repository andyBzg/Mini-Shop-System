using Application.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetUserByEmail(string email);
        void Add(Guid guid, User user);
    }
}
