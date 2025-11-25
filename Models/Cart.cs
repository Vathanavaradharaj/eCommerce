using System;

namespace eCommerce.Models
{
        public class Cart
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public required string ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get; set; }
        public DateTime? AddedDate { get; set; }

    }
}
