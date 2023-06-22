using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderAPI.Models
{
    public class OrderDetail
    {
        [Key]
        public long OrderDetailsId { get; set; }

        public long OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
