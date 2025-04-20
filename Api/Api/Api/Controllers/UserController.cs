using Microsoft.AspNetCore.Mvc;
using UserServiceClient;

namespace Api.Controllers;

[Route("api/user")]
public class UserController : Controller
{
    private readonly DBUserService.DBUserServiceClient _userService; 
    
    public UserController(DBUserService.DBUserServiceClient userService)
    {
        _userService = userService;
    }
    
    [HttpPost ("create")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        
        var response = await _userService.CreateUserAsync(new CreateUserRequest
        {
            Username = user.Username,
            Password = user.Password,
            Email = user.Email
        });
        
        
        return Created("", response.UserId);
    }
    
}