namespace Broooms.Cart;

public class Cart
{
    public Guid UserId { get; set; }

    public IList<CartItem> Items { get; set; } = new List<CartItem>();

    public Cart(Guid userId) => UserId = userId;
}
