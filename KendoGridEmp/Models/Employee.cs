using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoGridEmp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Contact { get; set; }
        public string Designation { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
    }
}
