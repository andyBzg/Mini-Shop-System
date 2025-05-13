using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{

    internal class AdminMenu
    {
        private readonly User _user;

        public AdminMenu(User user)
        {
            _user = user;
        }

        public void RunAdminMenu()
        {
            while (true)
            {
                string prompt = $"Welcome {_user.Username}! What would you like to do?";
                string[] options = { "Product Management", "User Management", "Statistics", "Back", "Exit" };

                Menu adminMenu = new Menu(prompt, options);
                int selectedIndex = adminMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        ManageProducts();
                        break;
                    case 1:
                        ManageUsers();
                        break;
                    case 2:
                        DisplayStatistics();
                        break;
                    case 3:
                        return;
                    case 4:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ManageProducts()
        {
            //TODO implement product management for admin
            Console.WriteLine("Placeholder for product management section");
            ReturnToAdminMenu();
        }

        private void ManageUsers()
        {
            //TODO implement user management for admin
            Console.WriteLine("Placeholder for user management section");
            ReturnToAdminMenu();
        }

        private void DisplayStatistics()
        {
            //TODO implement statistics for admin
            Console.WriteLine("Placeholder for statistics section");
            ReturnToAdminMenu();
        }

        private void Exit()
        {
            WaitForKey("\nPress any key to exit ...");
            Environment.Exit(0);
        }

        private void WaitForKey(string? message = null)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }

        private void ReturnToAdminMenu()
        {
            WaitForKey("\nPress any key to return to admin menu.");
            RunAdminMenu();
        }
    }
}
