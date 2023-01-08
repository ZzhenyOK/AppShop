using System.ComponentModel.DataAnnotations;

namespace AppShop.Models
{
    public class Vendor : BaseModel
    {
        public string VendorName { get; set; }
        public string Email { get; set; }
    }
}
