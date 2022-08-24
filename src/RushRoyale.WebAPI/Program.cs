using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using RushRoyale.Application.Features.Identity;
using RushRoyale.Application.Features.News;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddDiscord(options =>
    {
        options.ClientId = builder.Configuration["Discord:ClientId"];
        options.ClientSecret = builder.Configuration["Discord:ClientSecret"];
    }).AddCookie();

builder.Services.AddSwaggerDocument();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddSingleton<NewsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Allowing cors");
    
    // Allow different port in development locally. Deployed url has the same origin
    app.UseCors(x => x
        .WithOrigins("https://localhost:7005", "https://localhost:7028")
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/ping", [Authorize] () => "Pong!");

app.Run();