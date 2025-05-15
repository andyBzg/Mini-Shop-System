using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly List<CartItem> _cartItems;
        private readonly IProductService _productService;

        public CartService(List<CartItem> cartItems, IProductService productService)
        {
            _cartItems = cartItems;
            _productService = productService;
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItems.ToList();
        }

        public bool AddToCart(Guid id, int quantity)
        {
            Product? product = _productService.GetProductById(id);
            if (product == null || product.Stock < quantity)
                return false;

            bool success = _productService.ReduceProductQuantity(product.Id, quantity);
            if (!success)
                return false;

            CartItem? cartItem = _cartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);

            if (cartItem != null)
                cartItem.Quantity += quantity;
            else
                _cartItems.Add(new CartItem(product, quantity));

            return true;
        }

        public bool RemoveFromCart(Guid id)
        {
            CartItem? cartItem = _cartItems.FirstOrDefault(ci => ci.Product.Id == id);
            if (cartItem == null)
                return false;

            bool sucess = _productService.IncreaseProductQuantity(cartItem.Product.Id, cartItem.Quantity);
            if (!sucess)
                return false;

            _cartItems.Remove(cartItem);
            return true;
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }
    }
}
