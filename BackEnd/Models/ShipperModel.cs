using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class ShipperModel
    {
        public int ShipperId { get; set; }
        [Required]
        public string CompanyName { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
