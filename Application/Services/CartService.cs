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

        public bool AddToCart(Guid id, int quantity)
        {
            Product? product = _productService.GetProductById(id);
            if (product == null || product.Stock < quantity) 
                return false;

            CartItem cartItem = new CartItem(product, quantity);
            _productService.ReduceProductQuantity(product.Id, quantity);
            _cartItems.Add(cartItem);
            return true;
        }

        public List<CartItem> GetCartItems() { 
            return _cartItems;
        }
    }
}
