using Application;
using Application.Interfaces;
using Application.Models;

namespace Presentation.UI.Menus
{
    internal class MainMenu : BaseMenu
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private IOrderService _orderService;

        public MainMenu(IUserService userService, IProductService productService, ICartService cartService, IOrderService orderService)
            : base("Welcome to Mini-Shop! What would you like to do?", ["Register", "Login", "About", "Exit"])
        {
            _userService = userService;
            _productService = productService;
            _cartService = cartService;
            _orderService = orderService;
        }

        public void RunMainMenu()
        {
            while (true)
            {
                int selectedIndex = Run();

                switch (selectedIndex)
                {
                    case 0:
                        RegisterNewUser();
                        break;
                    case 1:
                        Login();
                        break;
                    case 2:
                        DisplayAboutInfo();
                        break;
                    case 3:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
        }

        private void RegisterNewUser()
        {
            DisplayTitle("User Registration");
            Console.WriteLine("Enter Username: ");
            string username = (Console.ReadLine() ?? string.Empty).Trim();
            Console.WriteLine("Enter Email: ");
            string email = (Console.ReadLine() ?? string.Empty).Trim();
            Console.WriteLine("Enter Password: ");
            string password = (Console.ReadLine() ?? string.Empty).Trim();

            bool isSuccess = _userService.Register(username, email, password);
            if (isSuccess)
                Console.WriteLine("Registration Successful");
            else
                Console.WriteLine("Failure by Registration");

            ReturnToMainMenu();
        }

        private void Login()
        {
            DisplayTitle("Log in");
            Console.WriteLine("Enter Email: ");
            string email = (Console.ReadLine() ?? string.Empty).Trim();
            Console.WriteLine("Enter Password: ");
            string password = (Console.ReadLine() ?? string.Empty).Trim();

            User? currentUser = _userService.Login(email, password);

            if (currentUser != null)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {currentUser.Username}! ({currentUser.Role.ToString()})");
                Thread.Sleep(1000);

                if (currentUser.Role == UserRole.Admin)
                {
                    AdminMenu adminMenu = new AdminMenu(currentUser);
                    adminMenu.RunAdminMenu();
                }
                else
                {
                    CustomerMenu customerMenu = new CustomerMenu(currentUser.Id, _productService, _cartService, _orderService);
                    customerMenu.RunCustomerMenu();
                }
            }
            else
            {
                Console.WriteLine("Login failed.");
                ReturnToMainMenu();
            }
        }

        private void DisplayAboutInfo()
        {
            DisplayTitle("About");
            Console.WriteLine("This e-commerce demo app was created by Andrei Bezgatsev.");
            ReturnToMainMenu();
        }

        private void Exit()
        {
            DisplayTitle("Goodbye!");
            WaitForKey("\nPress any key to exit ...");
            Environment.Exit(0);
        }

        private void ReturnToMainMenu()
        {
            WaitForKey("\nPress any key to return to main menu.");
            RunMainMenu();
        }
    }
}
