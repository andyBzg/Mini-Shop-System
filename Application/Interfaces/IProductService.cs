using Application.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product? GetProductById(Guid id);
    }
}
