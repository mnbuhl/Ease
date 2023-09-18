using System.ComponentModel.DataAnnotations;
using Eaze.App.Common.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Eaze.App.Models;

public sealed class User : IdentityUser<Guid>, ITimestamped, IEntity<Guid>
{
    [MaxLength(255)]
    public string? Name { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}