using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nerdstore.WebApp.Services;

namespace Nerdstore.WebApp.Pages;

public class ProductPage : PageModel
{
    private readonly ILogger<ProductPage> _logger;
    private readonly ProductService _productService;
    private readonly CartService _cartService;

    public ProductPage(ILogger<ProductPage> logger, ProductService productService, CartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    private async Task Load(Guid id)
    {
        SelectedProduct = _productService.GetProduct(id);
        RelatedProducts = _productService.GetRelatedProduct(id);
    }
    
    public async Task OnGet(Guid id)
    {
        await Load(id);
    }

    public async Task OnPost(Guid id)
    {
        _cartService.AddProduct(id, Quantity);
        await Load(id);
    }
    
    [BindProperty]
    public List<Product> RelatedProducts { get; set; }
    [BindProperty]
    public Product? SelectedProduct { get; set; }
    [BindProperty]
    public int Quantity { get; set; } = 1;
    
}