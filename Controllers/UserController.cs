using EmployeeAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using EmployeeAdmin.Data;
using EmployeeAdmin.Common;
using EmployeeAdmin.Models.Entities;
using EmployeeAdmin.Services;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Authorization;
using EmployeeAdmin.Helpers;



namespace EmployeeAdmin.Controllers
{
    //localhost:xxxx/api/User
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserService _userservice;
       

        public UserAPIController(IUserService userservice) // Constructor
        {
            _userservice = userservice;
            
        }
        
        
        [HttpGet]
        [Route("getusers")]
        
        public IActionResult GetUsers(int? id = null, string status = "all")
        {
         
            try
            {
                var user = _userservice.GetUsers(id, status);
                if (user == null)
                {
                    return NotFound("No user exists");
                }
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                
                    
                    return BadRequest(ex.Message);
                
            }
        }



            [HttpPost]
            [Route("addnewuser")]
            public  IActionResult AddUser(AddUserDto adduserdto)
            {
                try
                {
                    var result = _userservice.AddUser(adduserdto); // Call the service method
                    return Ok(result); // Return the result
                }
                catch (Exception ex)
                {
                    // Handle exceptions, possibly log them
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
           
           
           [HttpPut]
           [Route("updateuser")]
            public IActionResult UpdateUser(int id, string? country = null, Status? stat = null, string? userName = null)
               {
            // Find the user by ID
            try
            {
                var result = _userservice.UpdateUser(id, country, stat, userName);
                return Ok(result);

            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
                       

               }


                        // Save changes to the database


              [HttpDelete] // permanent delete
              [Route("deleteuser")]

              public IActionResult DeleteUser(int Id)
        {
              try
                 {
                _userservice.DeleteUser(Id);
                return Ok("UserDeleted");
                   }
              catch(Exception ex)
                {
                return StatusCode(500, $"Internal server error: {ex.Message}");
                }

        }
                      

                      [HttpDelete]
                       [Route("softdelete")] // soft delete
                       public IActionResult SoftDeleteUser(int Id)
                       {
                           try
                              {
                _userservice.SoftDeleteUser(Id);
                                   return Ok("User is terminated");
                              }

                            catch (Exception ex)
                              {
                                    return StatusCode(500, $"Internal server error: {ex.Message}");
                               }
        }
                      
        }
    }
