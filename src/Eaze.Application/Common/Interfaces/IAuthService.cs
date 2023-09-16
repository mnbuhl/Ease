﻿using Eaze.Application.Requests;
using Eaze.Domain.Models;

namespace Eaze.Application.Common.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginRequest loginRequest);
    Task<User> Register(RegisterRequest request);
    Task Logout();
    Task<string> GeneratePasswordResetToken(User user);
    Task<string> GenerateEmailConfirmationToken(User user);
    Task<User> ConfirmEmail(Guid userId, string token);
}