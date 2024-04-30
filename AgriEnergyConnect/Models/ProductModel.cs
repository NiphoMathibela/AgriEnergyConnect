namespace AgriEnergyConnect.Models
{
    public class ProductModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public int? UserId { get; set; }
    }
}
