using Api.Dtos;
using Api.Services; 
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/subscribe")]
public class SubscribeController(IQueueService queueService) : Controller
{
    
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] UserEvent user)
    {
        try
        {
            await ValidateUser();
            await queueService.Send(user);

            return Created("", "subscribed");
        }
        catch
        {
            return Challenge();
        }
    }

    private async Task ValidateUser()
    {
        
    }
    
}