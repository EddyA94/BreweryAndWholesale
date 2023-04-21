using Microsoft.EntityFrameworkCore;
using BreweryWholesale.Infrastructure;
using BreweryWholesale.Infrastructure.Repository;
using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Infrastructure.Services;
using BreweryWholesale.Infrastructure.UnitsOfWork;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<BrewerWholesaleDBContext>(options => { options.UseSqlServer("Data Source=localhost;Initial Catalog=Brewery_new;Persist Security Info=True;User ID=admin;Password=admin;Encrypt=false"); options.LogTo(Console.WriteLine); });

builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddTransient<IBeerService, BeerServices>();
builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();
builder.Services.AddTransient<IBreweryService, BreweryServices>();
builder.Services.AddScoped<IWholesalerStockRepository, WholesalerStockRepository>();
builder.Services.AddTransient<IWholesalerStockService, WholesalerStockServices>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddTransient<ISaleService, SaleServices>();
builder.Services.AddScoped<ITransactionUnitOfWork, TransactionUnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
