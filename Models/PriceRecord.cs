using System.ComponentModel.DataAnnotations.Schema;

namespace SmartShopping.Models
{
    public class PriceRecord
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public string Store { get; set; }
        public float Price { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime CheckDate { get; set; }
    }
}
