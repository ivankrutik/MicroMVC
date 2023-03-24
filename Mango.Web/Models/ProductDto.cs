using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }

        public long ProductID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public byte[]? Image { get; set; }

        [Range(1, 100)]
        public int Count { get; set; }
    }
}
