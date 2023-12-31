﻿using System.ComponentModel.DataAnnotations;
using Ease.App.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Ease.App.Models;

public sealed class User : IdentityUser<Guid>, ITimestamped, IEntity<Guid>
{
    [MaxLength(255)]
    public string? Name { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}