using Eaze.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Eaze.Domain.Models;

public sealed class User : IdentityUser<Guid>, ITimestamped, IEntity<Guid>
{
    public string? Name { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}