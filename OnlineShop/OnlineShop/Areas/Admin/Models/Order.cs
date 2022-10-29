using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Areas.Admin.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
        [Key]
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PhoneNo { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
