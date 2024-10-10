using EmployeeAdmin.Common;
using EmployeeAdmin.Data;
using EmployeeAdmin.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeAdmin.Services;
using EmployeeAdmin.Models;
using System.Diagnostics;

namespace EmployeeAdmin.Services
{
    public interface IUserService
    {
        public List<Employee> GetUsers(int? id = null, string status = "all");
        public List<Employee> AddUser(AddUserDto adduserdto);
        public List<Employee> UpdateUser(int id, string? country = null, Status? stat = null, string? userName = null);

        public void DeleteUser(int id);
        public void SoftDeleteUser(int id);
        public Task<SystemUser> AuthenticateUserAsync(int Id, string userName);


        /* Task<Employee> AddUser(AddUserDto adduserdto);
         Task<Employee> UpdateUser(int id, string? country = null, Status? stat = null, string? userName = null);

         Task<Employee> DeleteUser(int id);
        */
    }
}

namespace EmployeeAdmin.Services
{
    public class UserService : IUserService
    {

        private UserDbcontext dbcontext;

        public UserService (UserDbcontext dbcontext) // Constructor
        {
            this.dbcontext = dbcontext;
        }
        public List<Employee> GetUsers(int? id = null, string status = "all")
        {
            IQueryable<Employee> usersQuery = dbcontext.Employees;

            // Filter based on the status query parameter
            if (id.HasValue)
            {
                var userfind = dbcontext.Employees.Find(id.Value);
                if (userfind == null)
                {
                    throw new Exception("user not found");
                }
                return new List<Employee> { userfind };
            }

            if (status == "active")
            {
                usersQuery = usersQuery.Where(user => user.Stat.Equals(Status.Active));
            }
            else if (status == "inactive")
            {
                usersQuery = usersQuery.Where(user => user.Stat.Equals(Status.Inactive));
            }
            else if (status == "terminated")
            {
                usersQuery = usersQuery.Where(user => user.Stat.Equals(Status.Terminate));
            }

            var userCount = usersQuery.Count();
            if (userCount == 0)
            {
                throw new Exception("No users exist");
            }

            var usersList = usersQuery.ToList();
            return usersList;
        }

      
        public List<Employee> AddUser(AddUserDto adduserdto)
         {
             // Check if user exists
           

             // Create a new user
             var newUser = new Employee
             {
                 UserName = adduserdto.UserName,
                 Country = adduserdto.Country,
                 Stat = Status.Active,
                 CreatedAt = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt")
             };
            
            // Add new user to the database
            var user = dbcontext.Employees.FirstOrDefault(user => user.UserName == adduserdto.UserName);
            if (user != null)
            {
                throw new Exception("UserName is already exists");
            }
            dbcontext.Employees.Add(newUser);
            dbcontext.SaveChanges();
            return new List<Employee> { newUser };
        }
     
        public List<Employee> UpdateUser(int id, string? country = null, Status? stat = null, string? userName = null)
         {
             // Find the user by ID
             var user =  dbcontext.Employees.Find(id);
             if (user == null)
             {
                throw new Exception("User not found");
             }
             if (user.Stat == Status.Terminate)
             {
                 throw new Exception("User not exist in system");
             }
            


             
             // Update properties only if they are provided
             if (!string.IsNullOrEmpty(country))
             {
                 if (country.Length < 4 || country.Length > 50)
                 {
                      throw new Exception("Invalid country length");
                 }
                 user.Country = country;
             }

             if (!string.IsNullOrEmpty(userName))
             {
                var existingUser = dbcontext.Employees
                .FirstOrDefault(user => user.UserName == userName);
               
                if (existingUser != null)
                {
                    Debug.WriteLine(existingUser);
                    throw new Exception("User with this username already exists");
                }

                if (userName.Length < 4 || userName.Length > 21)
                 {
                     throw new Exception ("Invalid username length");
                 }
                 user.UserName = userName;
             }

             if (stat.HasValue)
             {
                 user.Stat = stat.Value;
             }

            
             dbcontext.SaveChanges();

            return new List<Employee> { user };
         }
        
         public void  DeleteUser(int Id)
          {

              var user =  dbcontext.Employees.Find(Id);
            if (user is null) throw new Exception("User not found");
              dbcontext.Employees.Remove(user);
              dbcontext.SaveChanges();
              
          }

          public void SoftDeleteUser(int Id)
        {
            var user = dbcontext.Employees.Find(Id);
            if (user is null) throw new Exception("User Not Found");
            user.Stat = Status.Terminate;
            dbcontext.Employees.Remove(user);
            dbcontext.SaveChanges();
        }

       public async Task<SystemUser> AuthenticateUserAsync(int Id ,string userName){
            var findId = await dbcontext.Employees.FindAsync(Id);
            if (findId == null) throw new Exception("User not found");
            var findUser = await dbcontext.Employees.FirstOrDefaultAsync(user=> user.UserName == userName);
            if (findUser == null) throw new Exception("User not found");
            var user = new SystemUser
            {
                Id = findUser.Id,
                Name = findUser.UserName
            };
            return user;
        }
    }

}
