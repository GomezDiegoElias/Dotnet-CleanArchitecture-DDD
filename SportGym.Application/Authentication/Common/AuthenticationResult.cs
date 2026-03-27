using SportGym.Domain.Entities;

namespace SportGym.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);
