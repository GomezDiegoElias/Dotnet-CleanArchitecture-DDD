using SportGym.Domain.Entities;

namespace SportGym.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
  User? GetUserByEmail(string email);
  void Add(User user);
}