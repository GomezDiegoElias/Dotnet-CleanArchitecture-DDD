using SportGym.Domain.Entities;

namespace SportGym.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
