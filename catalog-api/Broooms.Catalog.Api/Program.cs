using Broooms.Catalog.Core.Data;
using Broooms.Catalog.Core.Services;
using Broooms.Catalog.Infrastructure.Data;
using Broooms.Catalog.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CatalogDataContext>(
    cfg => cfg.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<EntityToDtoProfile>());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
