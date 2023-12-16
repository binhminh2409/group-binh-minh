using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EcommerceWeb.Models
{
    public partial class genMainSlider
    {
        [Key]
        public int MainSliderID { get; set; }
        public string ImageURL { get; set; }
        public string AltText { get; set; }
        public string OfferTag { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BtnText { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    }
}