using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmployeeAdmin.Common;
namespace EmployeeAdmin.Models
{

    public class AddUserDto
    {
        [StringLength(20,MinimumLength = 3, ErrorMessage = "Error not valid name")]
        public required string UserName { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Error not valid country")]
        public required string Country { get;  set; }

        //DTOS --> Data transfer objects which is used for transfering into diiferent parts of applications mostly used for 
        //Updation and Adding data in database.

       








    }
}
