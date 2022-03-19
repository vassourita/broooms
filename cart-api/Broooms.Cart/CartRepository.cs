namespace Broooms.Cart;

using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

public class CartRepository
{
    private readonly IDistributedCache _redisCache;

    public CartRepository(IDistributedCache cache) =>
        _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));

    private static string GetKey(Guid userId) => $"cart:{userId}";

    public async Task<Cart> GetCart(Guid userId)
    {
        var cart = await _redisCache.GetStringAsync(GetKey(userId));
        if (string.IsNullOrEmpty(cart))
        {
            return new Cart(userId);
        }

        return JsonConvert.DeserializeObject<Cart>(cart);
    }

    public async Task<Cart> UpdateCart(Guid userId, CartItem cartItem)
    {
        var existingCart = await GetCart(userId);

        var existingItem = existingCart.Items.FirstOrDefault(
            x => x.ProductId == cartItem.ProductId
        );
        if (existingItem != null)
        {
            existingCart.Items = existingCart.Items
                .Where(x => x.ProductId != cartItem.ProductId)
                .ToList();
            var newQuantity = existingItem.Quantity + cartItem.Quantity;
            if (newQuantity > 0)
            {
                existingCart.Items.Add(
                    new CartItem { ProductId = cartItem.ProductId, Quantity = newQuantity }
                );
            }
        }
        else if (cartItem.Quantity > 0)
        {
            existingCart.Items.Add(cartItem);
        }

        await _redisCache.SetStringAsync(GetKey(userId), JsonConvert.SerializeObject(existingCart));

        return await GetCart(userId);
    }

    public async Task DeleteCart(Guid userId) => await _redisCache.RemoveAsync(GetKey(userId));
}
