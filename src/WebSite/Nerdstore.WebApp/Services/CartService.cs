namespace Nerdstore.WebApp.Services;

public class CartService
{
    private readonly ProductService _productService;

    public CartService(ProductService productService)
    {
        _productService = productService;
        CartId = Guid.NewGuid();
    }

    public Guid CartId { get; set; }
    public List<CartItem> Products { get; set; } = new();

    public int Quantity
    {
        get => Products.Sum(p => p.Quantity);
    }

    public decimal Total
    {
        get => Products.Sum(x => x.Quantity * x.Product.Price);
    }

    public bool SetQuantity(Guid productId, int quantity)
    {
        var found = Products.FirstOrDefault(p => p.Id == productId);
        if (found == null) return false;
        if (quantity == 0){ Products.Remove(found); return true; }
        found.SetQuantity(quantity);
        return true;
    }

    public bool AddProduct(Guid productId, int quantity)
    {
        var found = Products.FirstOrDefault(p => p.Id == productId);
        if (quantity == 0 && found is null) return true;
        else if (quantity == 0 && found is not null)
        {
            Products.Remove(found);
            return true;
        }

        if (_productService.Exists(productId))
        {
            if (found is null)
                Products.Add(new(_productService.GetProduct(productId), quantity));
            else
                found.AddQuantity(quantity);
            return true;
        }

        return false;
    }
}

public class CartItem(Product product, int quantity)
{
    public Guid Id => Product.Id;
    public decimal Total => Quantity * Product.Price;
    public int Quantity { get; private set; } = quantity;
    public Product Product { get; } = product;

    public void AddQuantity(int quantity)
        => Quantity += quantity;

    public bool SetQuantity(int quantity)
    {
        Quantity = quantity;
        return true;
    }
}