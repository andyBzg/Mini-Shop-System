using Application.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Dictionary<Guid, User> GetAll();
        User? GetUserByEmail(string email);
        void Add(Guid guid, User user);
    }
}
