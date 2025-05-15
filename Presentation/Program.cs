using Application.Interfaces;
using Application.Models;
using Application.Services;
using Infrastructure.Repositories;
using Presentation.Views;


string userStorageFilePath = "user_database.json";
IUserRepository userRepository = new UserRepository(userStorageFilePath);
IPasswordHasher passwordHasher = new PasswordHasher();
IUserService userService = new UserService(userRepository, passwordHasher);

string productStorageFilePath = "product_database.json";
IProductRepository productRepository = new ProductRepository(productStorageFilePath);
IProductService productService = new ProductService(productRepository);

List<CartItem> cartItems = new List<CartItem>();
ICartService cartService = new CartService(cartItems, productService);

string orderRepositoryFilePath = "order_database.json";
IOrderRepository orderRepository = new OrderRepository(orderRepositoryFilePath);
IOrderService orderService = new OrderService(orderRepository, productService);

MainMenu mainMenu = new MainMenu(userService, productService, cartService, orderService);
mainMenu.RunMainMenu();
