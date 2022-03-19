namespace Broooms.Cart;

public class Cart
{
    public Guid UserId { get; set; }
    public IList<CartItem> Items { get; } = new List<CartItem>();

    public Cart(Guid userId) => UserId = userId;

    public void AddItem(CartItem item) => Items.Add(item);

    public void RemoveItem(CartItem item) => Items.Remove(item);

    public void Clear() => Items.Clear();

    public void UpdateItemQuantity(CartItem item, int quantity) => item.Quantity = quantity;
}
