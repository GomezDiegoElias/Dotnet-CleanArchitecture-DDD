using SportGym.Application.Common.Interfaces.Services;

namespace SportGym.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime UtcNow => DateTime.UtcNow;
}