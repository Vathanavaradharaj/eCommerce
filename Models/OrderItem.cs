public class OrderItem
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string ProductId { get; set; } = string.Empty;

    public int Quantity { get; set; }   // <-- ADD THIS

    public decimal Price { get; set; }

    public decimal Total { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime ExpectedDeliveryDate { get; set; }
}
