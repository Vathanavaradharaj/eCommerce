namespace eCommerce.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; }
        public string? ItemImage { get; set; }
         public string UserId { get; set; } = string.Empty;
    }
}
