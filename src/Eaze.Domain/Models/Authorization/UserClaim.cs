using Eaze.Domain.Contracts;

namespace Eaze.Domain.Models.Authorization;

public sealed class UserClaim : BaseEntity, ITimestamped
{
    public required string Type { get; set; }
    public required string Value { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}