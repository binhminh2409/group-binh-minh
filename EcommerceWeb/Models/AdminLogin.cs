using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EcommerceWeb.Models
{
    public partial class AdminLogin
    {
        [Key]
        public int LoginID { get; set; }
        public int EmpID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> RoleType { get; set; }
        public string Notes { get; set; }

        public virtual AdminEmployee AdminEmployee { get; set; }
        public virtual Role Role { get; set; }
    }
}
