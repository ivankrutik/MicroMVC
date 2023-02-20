namespace Mango.Web.Models
{
    public class ProductDto
    {
        public long ProductID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public byte[]? Image { get; set; }
    }
}
