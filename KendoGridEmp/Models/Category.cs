using System.ComponentModel.DataAnnotations;

namespace KendoGridEmp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
