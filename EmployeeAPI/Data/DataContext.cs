using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Models;

namespace EmployeeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
    : base(options)
        {
        }

        public DbSet<userModel> users { get; set; }
        public DbSet<employeeModel> employees { get; set; }
    }
}
