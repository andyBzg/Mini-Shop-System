using Application.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User? Login(string email, string password);
        bool Register(string username, string email, string plainPassword);
    }
}
