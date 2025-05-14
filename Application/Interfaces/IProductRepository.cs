using Application.Models;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        List<Product> LoadAll();
        Product? GetById(Guid id);
        void Save(Product product);
        void Update(Product product);
    }
}
