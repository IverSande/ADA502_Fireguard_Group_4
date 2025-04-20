using Api.Services;

namespace Api.Dtos;

public record UserEvent(string UserId, string SubscriptionLocation) : QueueMessage
{
    public override string ToString()
    {
        return base.ToString();
    }
};