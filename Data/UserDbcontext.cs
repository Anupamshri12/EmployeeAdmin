using EmployeeAdmin.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdmin.Data
{
   
    public class UserDbcontext : DbContext
        //Here DbContext is a base class which is responsible for database connections and saves the changes ,queries to data.
    {
        //This is a constructor used to configure database connections.
        public UserDbcontext(DbContextOptions<UserDbcontext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public object Employee { get; internal set; }
     
        
    }
}
