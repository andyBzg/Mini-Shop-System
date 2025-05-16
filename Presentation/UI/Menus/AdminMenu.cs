using Application.Models;

namespace Presentation.UI.Menus
{

    internal class AdminMenu : BaseMenu
    {
        private readonly User _user;

        public AdminMenu(User user)
            : base($"Welcome {user.Username}! What would you like to do?", ["Product Management", "User Management", "Statistics", "Back", "Exit"])
        {
            _user = user;
        }

        public void RunAdminMenu()
        {
            while (true)
            {
                int selectedIndex = Run();

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
            DisplayTitle("Product Management");
            //TODO implement product management for admin
            ReturnToAdminMenu();
        }

        private void ManageUsers()
        {
            DisplayTitle("User Management");
            //TODO implement user management for admin
            ReturnToAdminMenu();
        }

        private void DisplayStatistics()
        {
            DisplayTitle("Statistics");
            //TODO implement statistics for admin
            ReturnToAdminMenu();
        }

        private void Exit()
        {
            WaitForKey("\nPress any key to exit ...");
            Environment.Exit(0);
        }

        private void ReturnToAdminMenu()
        {
            WaitForKey("\nPress any key to return to admin menu.");
            RunAdminMenu();
        }
    }
}
