using Microsoft.AspNetCore.Identity;

namespace Nerdstore.Authentication.WebApp.Data;

public sealed class ApplicationUser : IdentityUser
{
    public string? Initials { get; set; }
}