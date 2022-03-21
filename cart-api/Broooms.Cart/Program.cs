using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Broooms.Cart;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(
    options => options.Configuration = builder.Configuration.GetConnectionString("RedisConnection")
);
builder.Services.AddScoped<CartRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart API", Version = "v1" })
);

var app = builder.Build();

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

app.MapGet(
        "/api/v1/carts/{userId:guid}",
        async ([FromServices] CartRepository repo, [FromRoute] Guid userId) =>
        {
            var cart = await repo.GetCart(userId);
            return Results.Ok(cart ?? new Cart(userId));
        }
    )
    .AllowAnonymous()
    .Produces<Cart>((int)HttpStatusCode.OK, contentType: "application/json");

app.MapDelete(
        "/api/v1/carts/{userId:guid}",
        async ([FromServices] CartRepository repo, [FromRoute] Guid userId) =>
        {
            await repo.DeleteCart(userId);
            return Results.NoContent();
        }
    )
    .AllowAnonymous()
    .Produces((int)HttpStatusCode.NoContent, contentType: "application/json");

app.MapPut(
        "/api/v1/carts/{userId:guid}/products",
        async (
            [FromServices] CartRepository repo,
            [FromRoute] Guid userId,
            [FromBody] CartItem item
        ) => Results.Ok(await repo.UpdateCart(userId, item))
    )
    .AllowAnonymous()
    .Produces<Cart>((int)HttpStatusCode.Created, contentType: "application/json");

app.MapPost(
    "/api/v1/carts/{userId:guid}/coupons/{code:string}",
    ([FromServices] CartRepository repo, [FromRoute] Guid userId, [FromRoute] string code) =>
    {
        return;
    }
);

app.MapDelete(
    "/api/v1/carts/{userId:guid}/coupons",
    ([FromServices] CartRepository repo, [FromRoute] Guid userId) =>
    {
        return;
    }
);

app.MapPost(
    "/api/v1/carts/{userId:guid}/checkout",
    ([FromServices] CartRepository repo, [FromRoute] Guid userId) =>
    {
        return;
    }
);

app.Run();
