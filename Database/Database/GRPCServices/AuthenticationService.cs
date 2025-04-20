using AuthenticationServiceClient;
using Database.Entities;
using Grpc.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Database.GRPCServices;

public class AuthenticationService : AuthenticationServiceClient.AuthenticationService.AuthenticationServiceBase
{
    public AuthenticationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    private readonly ApplicationDbContext _dbContext;
    
    public override async Task<CreateAccessTokenResponse> CreateAccessToken(CreateAccessTokenRequest request, ServerCallContext context)
    {
        try
        {
            var user = await _dbContext.UserDataTable.FirstAsync(a => a.Id == request.UserId && a.Password == request.Password);
            var accessToken = Guid.CreateVersion7().ToString();

            await _dbContext.AccessTokenDataTable.AddAsync(new AccessToken
            {
                Token = accessToken,
                Expires = DateTimeOffset.Now.AddMinutes(30),
            });
            await _dbContext.SaveChangesAsync(context.CancellationToken);
            return new CreateAccessTokenResponse
            {
                AccessToken = accessToken
            };

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ("User not found");
        }
        
    }

    public override async Task<ValidateAccessTokenResponse> ValidateAccessToken(ValidateAccessTokenRequest request, ServerCallContext context)
    {
        try
        {
            var token = await _dbContext.AccessTokenDataTable.FirstAsync(a => a.Token == request.AccessToken);
            if (token.Expires < DateTimeOffset.Now)
                throw new Exception("Access token expired");
            return new ValidateAccessTokenResponse()
            {
                IsValid = true
            };
        }
        catch
        {
            return new ValidateAccessTokenResponse()
            {
                IsValid = false 
            };
            
        }
    }
}