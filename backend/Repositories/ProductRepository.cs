using ProductCatalog.Api.Models;
using System.Collections.Concurrent;
namespace ProductCatalog.Api.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<int, Product> _products = new();
    private int _nextId = 1;
    public ProductRepository()
    {
        Add(new Product { Code = "PROD001", Name = "Laptop", Price = 3499.99m });
        Add(new Product { Code = "PROD002", Name = "Mysz bezprzewodowa", Price = 149.99m });
    }
    public IEnumerable<Product> GetAll() => _products.Values.ToList();
    public Product Add(Product product)
    {
        product.Id = Interlocked.Increment(ref _nextId) - 1;
        _products[product.Id] = product;
        return product;
    }
}