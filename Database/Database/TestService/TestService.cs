using Grpc.Core;
using TestServiceClient;

namespace Database.TestService;

public class TestService : TestServiceClient.TestService.TestServiceBase
{
    private readonly ApplicationDbContext _dbContext;

    public TestService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override async Task<DataResponse> SendData(DataRequest request, ServerCallContext context)
    {
        var temp = _dbContext.TestDataTable.FirstOrDefault(c => c.Id == long.Parse(request.DataId));
        var response = new DataResponse() {Temperature = temp?.Temperature};
        await Task.Delay(100);
        return response;
    }
}