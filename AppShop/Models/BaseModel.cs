using System.ComponentModel.DataAnnotations;

namespace AppShop.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
