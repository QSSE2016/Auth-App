using AuthAppAPI.Data;
using AuthAppAPI.Repositories.Implementation;
using AuthAppAPI.Repositories.Interface;
using AuthAppAPI.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

// Add Repositories
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ILoginRepository,LoginRepository>();

// I feel like these services are better off being instantiated once.
builder.Services.AddSingleton<PasswordHasher>();
builder.Services.AddSingleton<PasswordVerifier>();
builder.Services.AddSingleton<JwtGenerator>();


var app = builder.Build();

// Avoid CORS bull (accept requests of any kind and allow credentials aka cookie related stuff)
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
