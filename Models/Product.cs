namespace eCommerce.Models
{
    public class Product
    {
        public required string Id { get; set; }
        public required string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
    public byte[]? ItemImage { get; set; }  
        public required string UserId { get; set; }

}
}