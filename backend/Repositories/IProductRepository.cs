using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product Add(Product product);
}
