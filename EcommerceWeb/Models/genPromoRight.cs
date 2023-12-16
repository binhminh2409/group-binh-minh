using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EcommerceWeb.Models
{
    public partial class genPromoRight
    {
        [Key]
        public int PromoRightID { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public string AltText { get; set; }
        public string OfferTag { get; set; }
        public string Title { get; set; }
        public Nullable<bool> isDeleted { get; set; }

        public virtual Category Category { get; set; }
    }
}