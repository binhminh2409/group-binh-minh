using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EcommerceWeb.Models
{
    public partial class Role
    {
        public Role()
        {
            this.AdminLogin = new HashSet<AdminLogin>();
        }
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AdminLogin> AdminLogin { get; set; }
    }
}