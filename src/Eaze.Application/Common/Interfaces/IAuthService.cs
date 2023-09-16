﻿using Eaze.Application.Features.Auth;
using Eaze.Domain.Models;

namespace Eaze.Application.Common.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginRequest loginRequest);
    Task<User> Register(RegisterRequest request);
    Task Logout();
}