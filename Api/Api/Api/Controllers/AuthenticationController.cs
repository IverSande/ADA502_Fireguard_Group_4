using Api.Dtos;
using AuthenticationServiceClient;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/authentication")]
public class AuthenticationController : Controller
{
    
    private readonly AuthenticationService.AuthenticationServiceClient _authenticationClient; 
    
    public AuthenticationController(AuthenticationService.AuthenticationServiceClient authenticationClient)
    {
        _authenticationClient = authenticationClient;
    }
    
    [HttpPost ("create")]
    public async Task<IActionResult> CreateAccessToken([FromBody] AccessRequest accessRequest)
    {
        
        var response = await _authenticationClient.CreateAccessTokenAsync(new CreateAccessTokenRequest 
        {
            UserId = accessRequest.UserId,
            Password = accessRequest.Password
        });


        return Created("", response.AccessToken);
    }
    
}