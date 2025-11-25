using System;

namespace eCommerce.DTOs
{
    public class CartDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get; set; }
        public DateTime? AddedDate { get; set; }
    }
}
