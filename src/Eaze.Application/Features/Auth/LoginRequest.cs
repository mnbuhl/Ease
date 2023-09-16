namespace Eaze.Application.Features.Auth;

public record LoginRequest(string Email, string Password, bool Remember);