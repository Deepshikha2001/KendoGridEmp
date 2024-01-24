using Microsoft.EntityFrameworkCore;

namespace KendoGridEmp.Models
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options)
             : base(options) //database connection



        {
        }
        public DbSet<Employee> Employees { get; set; }

    }
}
