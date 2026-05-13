using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Repositories;

namespace ProductCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Kod i Nazwa są wymagane.");
        }

        if (request.Price < 0)
        {
            return BadRequest("Cena nie może być ujemna.");
        }

        var product = new Product
        {
            Code = request.Code,
            Name = request.Name,
            Price = request.Price
        };

        var created = _repository.Add(product);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }
}
