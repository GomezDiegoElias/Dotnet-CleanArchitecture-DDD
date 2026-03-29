namespace BuberDinner.Domain.Common.ValueObjects;

using BuberDinner.Domain.Common.Models;

public sealed class AverageRating : ValueObject
{
    private AverageRating(double value, int numRatings)
    {
        this.Value = value;
        this.NumRatings = numRatings;
    }

    public double Value { get; private set; }

    public int NumRatings { get; private set; }

    public static AverageRating CreateNew(double rating = 0, int numRatings = 0)
    {
        return new AverageRating(rating, numRatings);
    }

    public void AddNewRating(Rating rating)
    {
        this.Value = ((this.Value * this.NumRatings) + rating.Value) / ++this.NumRatings;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}