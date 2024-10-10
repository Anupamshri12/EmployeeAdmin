using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeAdmin.Helpers;
using EmployeeAdmin.Services;



[ApiController]
[Route("api/controller")]
public class AuthController : ControllerBase
{
    private AuthHelper authHelper;
    private  IUserService userService;
     public AuthController(AuthHelper auth_helper ,IUserService user_service) {
        authHelper = auth_helper;
        userService = user_service;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(int Id ,string userName)
    {
        try
        {
            var get_user = await userService.AuthenticateUserAsync(Id, userName);
            if (get_user == null) return NotFound("User not found");
            var generate_token = authHelper.GenerateJWTToken(get_user);
            if (generate_token == null) return BadRequest("Token not generated");
            return Ok(generate_token);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
