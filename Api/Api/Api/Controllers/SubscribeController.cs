using Api.Dtos;
using Api.Services; 
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/subscribe")]
public class SubscribeController(IQueueService queueService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetTest()
    {
        
        await queueService.Send(new User("1", "Bergen"));
        
        return Ok("subscribed");
    }
    
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] User user)
    {
        
        await queueService.Send(user);
        
        return Ok("subscribed");
    }
    
}