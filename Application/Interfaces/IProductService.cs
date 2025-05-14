using Application.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAvailableProducts();
        Product? GetProductById(Guid id);
        bool AddNewProduct(string name, string description, decimal price, int stock);
        bool UpdateProduct(Guid id, string name, string description, decimal price, int stock);
        bool DeleteProduct(Guid id);
    }
}
