using Microsoft.EntityFrameworkCore;
using BreweryWholesale.Infrastructure;
using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Infrastructure.Services;
using BreweryWholesale.Infrastructure.UnitsOfWork;
using BreweryWholesale.Api.Middleware;
using BreweryWholesale.Infrastructure.Repository.BeerRepo;
using BreweryWholesale.Infrastructure.Repository.BreweryRepo;
using BreweryWholesale.Infrastructure.Repository.SaleRepo;
using BreweryWholesale.Infrastructure.Repository.WholesalerRepo;
using BreweryWholesale.Infrastructure.Repository.UserRepo;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<BrewerWholesaleDBContext>(options => { options.UseSqlServer("Data Source=localhost,1433;Initial Catalog=Brewery;Persist Security Info=True;User ID=admin;Password=Admin1234;Encrypt=false"); options.LogTo(Console.WriteLine); });

builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddTransient<IBeerService, BeerServices>();

builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();
builder.Services.AddTransient<IBreweryService, BreweryServices>();

builder.Services.AddScoped<IWholesalerStockRepository, WholesalerStockRepository>();
builder.Services.AddTransient<IWholesalerStockService, WholesalerStockServices>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddTransient<ISaleService, SaleServices>();

builder.Services.AddScoped<IWholesalerRepository, WholesalerRepository>();
builder.Services.AddTransient<IWholesalerService, WholesalerServices>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserServices>();

builder.Services.AddTransient<IQuoteService, QuoteServices>();

builder.Services.AddTransient<ITransactionUnitOfWork, TransactionUnitOfWork>();

builder.Services.AddTransient<ExceptionHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandler>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BrewerWholesaleDBContext>();
    context.Database.EnsureCreated();
}

app.Run();
