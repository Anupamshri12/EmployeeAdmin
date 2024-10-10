using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeAdmin.Common;

//Can create multiple classes in a entity but for proper maintainence and structuring use different files and folders.
namespace EmployeeAdmin.Models.Entities
{
    
    [Table("users")]
   

    public class Employee
    {
        
        public  int Id { get; set; }
        public required string UserName { get; set; }
        public string? Country { get; set; }
        public required Status Stat { get; set; }
        public required string CreatedAt { get; set; }

        
    }
    
}
