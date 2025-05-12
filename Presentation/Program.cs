using Application.Interfaces;
using Application.Models;
using Application.Services;
using Infrastructure.Repositories;


string filePath = "user_database.json";
IUserRepository userRepository = new UserRepository(filePath);
IPasswordHasher passwordHasher = new PasswordHasher();
IUserService userService = new UserService(userRepository, passwordHasher);


void RegisterNewUser()
{
    Console.WriteLine("Enter Username: ");
    string username = (Console.ReadLine() ?? string.Empty).Trim();
    Console.WriteLine("Enter Email: ");
    string email = (Console.ReadLine() ?? string.Empty).Trim();
    Console.WriteLine("Enter Password: ");
    string password = (Console.ReadLine() ?? string.Empty).Trim();

    bool isSuccess = userService.Register(username, email, password);
    if (isSuccess)
        Console.WriteLine("Registration Successful");
    else
        Console.WriteLine("Failure by Registration");
}

void Login()
{
    Console.WriteLine("Enter Email: ");
    string email = (Console.ReadLine() ?? string.Empty).Trim();
    Console.WriteLine("Enter Password: ");
    string password = (Console.ReadLine() ?? string.Empty).Trim();

    User? currentUser = userService.Login(email, password);
    if (currentUser != null)    
        Console.WriteLine($"Welcome, {currentUser.Username}!");    
    else    
        Console.WriteLine("Failure by login");
    
}

//RegisterNewUser();
Login();