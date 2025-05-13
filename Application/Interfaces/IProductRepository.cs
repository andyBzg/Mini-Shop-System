using Application.Models;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        List<Product> LoadAllProducts();
        Product? GetProductById(Guid id);
        void Add(Product product);
        void Delete(Product product);
        //TODO void Update(Product product);
    }
}
