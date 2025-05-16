using Application.Interfaces;
using Application.Models;

namespace Presentation.UI.Menus
{
    internal class OrderHistoryMenu : BaseMenu
    {
        private readonly IOrderService _orderService;
        private readonly Guid _userId;

        public OrderHistoryMenu(IOrderService orderService, Guid userId)
            : base("Please select Order from List below (Press ESC to go back)\n", Array.Empty<string>())
        {
            _orderService = orderService;
            _userId = userId;
        }

        internal void RunOrderHistoryMenu()
        {
            while (true)
            {
                List<Order> orders = _orderService.GetConfirmedOrdersByUser(_userId);

                if (orders.Count == 0)
                {
                    DisplayTitle("Order history");
                    Console.WriteLine("Order history is empty");
                    WaitForKey("\nPress any key to continiue ...");
                    return;
                }

                Options = orders.Select((po, i) => $"Order #{i + 1}: {po.TotalPrice} EUR").ToArray();

                int index = Run();

                if (index == -1)
                    break;

                Order selectedOrder = orders[index];
                ShowOrderDetails(selectedOrder);
                WaitForKey("\nPress any key to continiue ...");
            }
        }

        private void ShowOrderDetails(Order order)
        {
            List<CartItem> items = order.Items;
            string status = order.IsConfirmed == false ? "The order is waiting to be placed." : "The order has been placed.";

            DisplayTitle("Order Details");
            Console.WriteLine("List of Products:\n");
            items.ForEach(item => Console.WriteLine($"{item.Product.Name} | Amount: {item.Quantity} | Price: {item.TotalPrice} EUR"));
            Console.WriteLine("------");
            Console.WriteLine($"Total Price: {order.TotalPrice} EUR");
            Console.WriteLine("------");
            Console.WriteLine($"Time created: {order.CreatedAt.ToString("dd.MM.yyyy | HH:mm")}");
            Console.WriteLine($"Order status: {status}");
        }
    }
}
