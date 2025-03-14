using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nerdstore.Authentication.WebApp.Data;
using Nerdstore.Authentication.WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(options
    => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapGroup("account")
    .MapIdentityApi<ApplicationUser>();

app.MapGet("account/me",
        async (ClaimsPrincipal claims, ApplicationDbContext dbContext, CancellationToken cancellationToken = default)
        => {
            var userId = claims.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value;
            var found = await dbContext.Users.FindAsync(userId, cancellationToken);
            return new ProfileResponse(found.Email);
        })
    .RequireAuthorization();

await app.RunAsync();