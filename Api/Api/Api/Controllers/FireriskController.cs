
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using TestServiceClient;

namespace Api.Controllers;

[Route("api/firerisk")]
public class FireriskController : Controller
{
    //private TestService.TestServiceClient _testServiceClient;
    
    [HttpGet("{id}", Name = nameof(GetAccount))]
    public async Task<IActionResult> GetAccount(string id)
    {
        using var channel = GrpcChannel.ForAddress("https://host.docker.internal:5001");
        var client = new TestService.TestServiceClient(channel);
        var request = new DataRequest() { DataId = id };

        var response = await client.SendDataAsync(request);
        
        return Ok(response);
    }
    
    
}