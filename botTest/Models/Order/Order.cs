namespace botTest.Models.Order
{
    public class Order
    {
        public string OrderId { get; set; }
        public long UserChatId { get; set; }
        public List<Product>? Products { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Order()
        {
            Products = new List<Product>();
        }
    }
}
