using Microsoft.AspNetCore.Authentication.JwtBearer;
using RushRoyale.Application;
using RushRoyale.Infrastructure;
using RushRoyale.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = builder.Configuration["Auth0:Authority"];
        c.Audience = builder.Configuration["Auth0:Audience"];
    });

builder.Services.AddSwaggerDocument();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<CurrentUserService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Cosmos"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
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

app.Run();