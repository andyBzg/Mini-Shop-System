using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Presentation.Views;


string filePath = "user_database.json";
IUserRepository userRepository = new UserRepository(filePath);
IPasswordHasher passwordHasher = new PasswordHasher();
IUserService userService = new UserService(userRepository, passwordHasher);
MainMenu mainMenu = new MainMenu(userService);

mainMenu.RunMainMenu();
