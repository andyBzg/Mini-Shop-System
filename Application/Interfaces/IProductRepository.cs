using Application.Models;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        List<Product> LoadAllProducts();
        Product? GetProductById(Guid id);
    }
}
