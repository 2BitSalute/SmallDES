using System.Numerics;
using System.Security.AccessControl;

namespace Mayak.ProbabilityDistributions.Discrete;

/// <summary>
/// Useful for deriving a distribution from a set of samples.
/// </summary>
/// <typeparam name="T">The sample value type, e.g., height in inches.</typeparam>
public class SampleSpaceProbabilityDistribution : DiscreteProbabilityDistribution<double>
{
    private readonly Dictionary<double, double> density = [];
    private readonly IList<double> data;

    public SampleSpaceProbabilityDistribution(IList<double> data)
    {
        this.data = data;
        var frequencies = new Dictionary<double, int>();

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

    public override double Mean => this.density.Aggregate(0.0, (acc, pair) => acc + (pair.Key * pair.Value));

    // TODO: this and other calculations can be memoized
    public override double Variance => Math.Pow(this.density.Aggregate(0.0, (acc, pair) => acc + (pair.Key * pair.Value * pair.Value)), 2) - Math.Pow(this.Mean, 2);

    public override double Density(double x)
    {
        if (density.TryGetValue(x, out var probability))
        {
            return probability;
        }

        return 0.0;
    }

    public override IEnumerator<double> GetEnumerator()
    {
        while (true)
        {
            yield return this.InverseDistribution(Uniform.NextDouble());
        }
    }

    private double InverseDistribution(double x)
    {
        // Oof, I think this will work.
        // Smalltalk is 1-indexed, so they add 1 here
        return this.data[(int)(x * this.data.Count)];
    }
}