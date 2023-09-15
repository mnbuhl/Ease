using Eaze.Domain.Contracts;

namespace Eaze.Domain.Models;

public sealed class Role : BaseEntity, ITimestamped
{
    public required string Name { get; set; }
    public required string NormalizedName { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public ICollection<User> Users { get; set; } = new List<User>();
}