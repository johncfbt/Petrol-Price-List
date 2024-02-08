using PetrolPrice.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PetrolPrice.Models
{
    public class PetrolStation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Station Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Price(¢/litre)")]
        public decimal Price { get; set; }

        [Display(Name = "Posted by")]
        public PetrolPriceUser PetrolPriceUser { get; set; }
    }
}
