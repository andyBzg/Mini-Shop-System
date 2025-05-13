using Application.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product? GetProductById(Guid id);
        void AddNewProduct(string name, string description, decimal price, int stock);
        bool DeleteProduct(Guid id);
    }
}
