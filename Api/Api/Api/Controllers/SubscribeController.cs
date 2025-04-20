using Api.Dtos;
using Api.Services;
using AuthenticationServiceClient;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/subscribe")]
public class SubscribeController(IQueueService queueService, AuthenticationService.AuthenticationServiceClient authenticationServiceClient) : Controller
{
    
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] UserEvent user, [FromHeader]string accessToken)
    {
        try
        {
            if(!await ValidateUser(accessToken))
            {
               throw new UnauthorizedAccessException();
            }
            await queueService.Send(user);

            return Created("", "subscribed");
        }
        catch
        {
            return Challenge();
        }
    }

    private async Task<bool> ValidateUser(string token)
    {
        return (await authenticationServiceClient.ValidateAccessTokenAsync(new ValidateAccessTokenRequest()
            { AccessToken = token })).IsValid;

    }
    
}