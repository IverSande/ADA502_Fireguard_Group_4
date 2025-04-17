using Grpc.Core;
using UserServiceClient;

namespace Database.GRPCServices;

public class DbUserService : UserServiceClient.DBUserService.DBUserServiceBase
{
    public DbUserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    private readonly ApplicationDbContext _dbContext;
    
    public override Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        return base.CreateUser(request, context);
    }

    public override Task<GetUserResponse?> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var user = _dbContext.UserDataTable.Find(request.UserId);
        var userResponse = user is null ? null : new GetUserResponse()
        {
            User = new User()
            {
                Username = user.Username,
                UserId = user.Id
            }
        };
        return Task.FromResult(userResponse) ?? throw new RpcException(new Status(StatusCode.NotFound, "User does not exist"));
    }

    public override Task<GetUserEventResponse> GetUserEvent(GetUserEventRequest request, ServerCallContext context)
    {
        return base.GetUserEvent(request, context);
    }

    public override Task<GetEventResponse> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        return base.GetEvent(request, context);
    }
}