
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using TestServiceClient;

namespace Api.Controllers;

[Route("api/firerisk")]
public class FireriskController : Controller
{
    private readonly TestService.TestServiceClient _testServiceClient;

    public FireriskController(TestService.TestServiceClient testServiceClient)
    {
            _testServiceClient = testServiceClient;
    }
    
    [HttpGet("{id}", Name = nameof(GetAccount))]
    public async Task<IActionResult> GetAccount(string id)
    {
        var request = new DataRequest() { DataId = id };

        var response = await _testServiceClient.SendDataAsync(request);
        
        return Ok(response);
    }
    
    
}