using Api.Services;

namespace Api.Dtos;

public record User(string UserId, string SubscriptionLocation) : QueueMessage
{
    public override string ToString()
    {
        return base.ToString();
    }
};