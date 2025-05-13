using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Presentation.Views;


string userStorageFilePath = "user_database.json";
string productStorageFilePath = "product_database.json";
IUserRepository userRepository = new UserRepository(userStorageFilePath);
IProductRepository productRepository = new ProductRepository(productStorageFilePath);
IPasswordHasher passwordHasher = new PasswordHasher();
IUserService userService = new UserService(userRepository, passwordHasher);
IProductService productService = new ProductService(productRepository);
MainMenu mainMenu = new MainMenu(userService, productService);

mainMenu.RunMainMenu();
