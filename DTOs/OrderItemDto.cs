namespace eCommerce.DTOs
{
  public class OrderItemDto
{
    public string? UserId { get; set; }
    public string? ProductId { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
}

}
