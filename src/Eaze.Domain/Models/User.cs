using System.ComponentModel.DataAnnotations;
using Eaze.Domain.Constants;
using Eaze.Domain.Contracts;

namespace Eaze.Domain.Models;

public sealed class User : BaseEntity, ITimestamped
{
    [EmailAddress]
    public required string Email { get; set; }
    
    [RegularExpression(RegexPatterns.Username)]
    public string? Username { get; set; }

    public string? Name { get; set; }
    public string Password { get; set; } = default!;
    
    public DateTime? EmailVerifiedAt { get; set; }
    public Guid SecurityStamp { get; set; } = Guid.NewGuid();
    
    [EmailAddress]
    public string NormalizedEmail { get; set; } = default!;
    
    [RegularExpression(RegexPatterns.Username)]
    public string? NormalizedUsername { get; set; }
    
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}