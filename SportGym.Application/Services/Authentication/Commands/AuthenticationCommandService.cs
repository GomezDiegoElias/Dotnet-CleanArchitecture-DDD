using ErrorOr;
using SportGym.Application.Common.Interfaces.Authentication;
using SportGym.Application.Common.Interfaces.Persistence;
using SportGym.Application.Services.Authentication.Common;
using SportGym.Domain.Common.Errors;
using SportGym.Domain.Entities;

namespace SportGym.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
  {
    // 1. Validate the user doesn't exist
    if (_userRepository.GetUserByEmail(email) is not null)
    {
      return Errors.User.DuplicateEmail;
    }

    // 2. Create user (generate unique ID) & Persist to DB
    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password
    };

    _userRepository.Add(user);

    // 3. Create JWT token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }

}