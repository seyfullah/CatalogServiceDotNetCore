using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlowardApi.Model
{
    public class Products
    {
        [Key]
        public long ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string? Image { get; set; }

    }
}
