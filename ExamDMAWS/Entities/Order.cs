using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamDMAWS.Entities
{
    [Table("OrderTbl")]
    public class Order
    {
        internal DateTime orderDelivery;

        [Key]
        public int OrderId { get; set; }
        [Required]
        public string ItemCode { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int ItemQty { get; set; }
        [Required]
        public string OrderDelivery { get; set; }
        [Required]
        public string OrderAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
