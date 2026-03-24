using SportGym.Domain.Entities;

namespace SportGym.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);