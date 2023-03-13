namespace botTest.Models.User
{
    class User
    {
        public long ChatId { get; set; }
        public string Name { get; set; }

        public ENextMessage Step { get; set; }
        public EBackStep BackStep { get; set; }
        public List<Product>? UserProducts { get; set; }
        public Product? UserProduct { get; set; }

    }
}
