using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public bool Register(string username, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) != null)
            {
                Console.WriteLine($"User with email {email} already exists");
                return false;
            }
            else
            {
                Guid guid = Guid.NewGuid();
                string hashedPassword = _passwordHasher.HashPassword(password);
                User user = new User(username, email, hashedPassword);
                _userRepository.Add(guid, user);
                return true;
            }
        }

        public User? Login(string email, string password)
        {
            User? user = _userRepository.GetUserByEmail(email);

            if (user != null)
            {
                if (_passwordHasher.VerifyPassword(password, user.PasswordHash))
                {
                    return user;
                }
                else
                {
                    Console.WriteLine("Incorrect Password!");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"User with {email} not exists");
                return null;
            }
        }
    }
}
