namespace BuberDinner.Domain.Common.ValueObjects;

using BuberDinner.Domain.Common.Models;

public sealed class Rating : ValueObject
{
    private Rating(double value)
    {
        this.Value = value;
    }

    public double Value { get; private set; }

    public static Rating CreateNew(double rating = 0)
    {
        return new Rating(rating);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}