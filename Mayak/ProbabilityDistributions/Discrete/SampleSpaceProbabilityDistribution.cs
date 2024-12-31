namespace Mayak.ProbabilityDistributions.Discrete;

/// <summary>
/// Useful for deriving a distribution from a set of samples.
/// </summary>
/// <typeparam name="T">The sample value type, e.g., height in inches.</typeparam>
public class SampleSpaceProbabilityDistribution<T> : DiscreteProbabilityDistribution<T> where T : notnull
{
    private readonly Dictionary<T, double> density = [];
    private readonly IList<T> data;

    public SampleSpaceProbabilityDistribution(IList<T> data)
    {
        this.data = data;
        var frequencies = new Dictionary<T, int>();

        foreach (var datum in data)
        {
            if (!frequencies.TryGetValue(datum, out int count))
            {
                count = 0;
            }

            frequencies[datum] = count + 1;
        }

        var totalSize = frequencies.Aggregate(0, (total, el) => total + el.Value);

        foreach (var el in frequencies)
        {
            this.density[el.Key] = 1.0 * el.Value / totalSize;
        }
    }

    public override double Density(T x)
    {
        if (density.TryGetValue(x, out var probability))
        {
            return probability;
        }

        return 0.0;
    }

    protected override T InverseDistribution(double x)
    {
        // Oof, I think this will work.
        // Smalltalk is 1-indexed, so they add 1 here
        return this.data[(int)(x * this.data.Count)];
    }
}