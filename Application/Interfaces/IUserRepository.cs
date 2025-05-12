using Application.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(Guid guid, User user);
    }
}
