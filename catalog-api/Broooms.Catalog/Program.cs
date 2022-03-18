using System.Text.Json.Serialization;
using Broooms.Catalog.Data;
using Broooms.Catalog.Providers;
using Broooms.Catalog.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDbConnection"))
);
builder.Services.AddScoped<IFileManager, AzureBlobFileManager>(
    s =>
        new AzureBlobFileManager(
            builder.Configuration.GetValue<string>(
                "Providers:AzureBlobFileManager:AzureConnection"
            ),
            builder.Configuration.GetValue<string>("Providers:AzureBlobFileManager:ContainerName")
        )
);

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
        options.OperationFilter<SwaggerFileOperationFilter>();
    }
);
builder.Services.Configure<ApiBehaviorOptions>(
    options => options.SuppressModelStateInvalidFilter = true
);

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<DataContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        }
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
