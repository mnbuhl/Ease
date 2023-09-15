using System.ComponentModel.DataAnnotations;
using Eaze.Domain.Constants;
using Eaze.Domain.Contracts;

namespace Eaze.Domain.Models;

public sealed class User : BaseEntity, ITimestamped
{
    [RegularExpression(RegexPatterns.Username)]
    public required string Username { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }
    
    public string Password { get; set; } = default!;
    
    public bool EmailConfirmed { get; set; }
    public Guid SecurityStamp { get; set; } = Guid.NewGuid();
    
    [RegularExpression(RegexPatterns.Username)]
    public string NormalizedUsername { get; set; } = default!;
    
    [EmailAddress]
    public string NormalizedEmail { get; set; } = default!;
    
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}