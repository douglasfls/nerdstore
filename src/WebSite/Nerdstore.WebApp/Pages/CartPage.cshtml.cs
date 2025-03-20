using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nerdstore.WebApp.Services;

namespace Nerdstore.WebApp.Pages;

public class CartPage : PageModel
{
    private readonly CartService _cartService;

    public CartPage(CartService cartService)
    {
        _cartService = cartService;
    }
    
    public void OnGet()
    { }
    
    [BindProperty]
    public Guid ProductId { get; set; }
    [BindProperty]
    public int Quantity { get; set; }
    
    public async Task OnPostAsync()
    {
        _cartService.SetQuantity(ProductId, Quantity);
        OnGet();
    }
}