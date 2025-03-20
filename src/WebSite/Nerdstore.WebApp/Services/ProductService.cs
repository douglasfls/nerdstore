using System.Text.Json;

namespace Nerdstore.WebApp.Services;

public class ProductService
{
    private readonly Dictionary<Guid, Product> _products;

    public ProductService()
    {
        using FileStream openStream = System.IO.File.OpenRead("products.json");
        _products = JsonSerializer.Deserialize<List<Product>>(openStream, JsonSerializerOptions.Web).ToDictionary(p=>p.Id) ;
        openStream.Close();
    }

    public IEnumerable<Product> GetProducts()
        => _products.Values;

    public Product? GetProduct(Guid id)
        => _products.TryGetValue(id, out var product) ? product : null;
    
    public List<Product> GetRelatedProduct(Guid id)
        => _products.Values
            .ToLookup(p=>p.Category)[GetProduct(id)?.Category]
            .Where(p=>p.Id != id)
            .Take(4)
            .ToList();

    public bool Exists(Guid productId)
    {
        return _products.ContainsKey(productId);
    }
}

public class Product
{
    public Product()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}