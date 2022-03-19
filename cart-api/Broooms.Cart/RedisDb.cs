namespace Broooms.Cart;

using System.Text.Json;
using StackExchange.Redis;

public class RedisDb
{
    private readonly IConnectionMultiplexer _redis;

    public RedisDb(IConnectionMultiplexer redis) => _redis = redis;

    private static readonly string CartKey = "cart";

    private static string GetCartKey(Guid userId) => $"{CartKey}:{userId}";

    public async Task<Cart?> GetCartAsync(Guid userId)
    {
        var db = _redis.GetDatabase();
        var cart = await db.StringGetAsync(GetCartKey(userId));
        if (cart.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<Cart>(cart);
    }

    public async Task SetCartAsync(Cart cart)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(GetCartKey(cart.UserId), JsonSerializer.Serialize(cart));
    }
}
