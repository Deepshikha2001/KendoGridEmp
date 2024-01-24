using System.ComponentModel.DataAnnotations;

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
    }
}
