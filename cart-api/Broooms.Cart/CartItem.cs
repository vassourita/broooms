namespace Broooms.Cart;

public class CartItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public CartItem(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public void IncrementQuantity() => Quantity++;

    public void DecrementQuantity() => Quantity--;
}
