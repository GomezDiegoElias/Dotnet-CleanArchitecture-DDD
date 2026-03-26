using SportGym.Domain.Entities;

namespace SportGym.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);