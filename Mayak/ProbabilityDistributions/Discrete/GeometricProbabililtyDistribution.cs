
namespace Mayak.ProbabilityDistributions.Discrete;

/// <summary>
/// Answers the question:
///     - How many repeated, independent Bernoulli trials are needed
///         before the first success is obtained?
///
/// This distribution is very handy for simulations: instead of asking
/// how many cars have arrived in the next 20 seconds, we can ask
/// how many seconds until the next car arrives.
/// </summary>
public class GeometricProbabilityDistribution : DiscreteProbabilityDistribution<int>
{
    private readonly BernoulliProbabilityDistribution bernoulli;
    private readonly bool inclusive;

    /// <summary>
    /// Exclusive vs. Inclusive:
    ///     Inclusive: starts with 1, determines which trial is the first success
    ///         "How many coin flips are needed to get heads?"
    ///     Exclusive: starts with 0, counts how many failures before first success
    ///         "How many shots will miss before scoring the first goal?"
    /// </summary>
    /// <param name="mean">The expected value of this distribution</param>
    /// <param name="inclusive">Inclusive is the default</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public GeometricProbabilityDistribution(double mean, bool inclusive = true)
    {
        if (mean < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(mean), "Geometric mean must be >= 1");
        }

        var probability = inclusive ? 1 / mean : 1 / (mean + 1);
        this.bernoulli = new BernoulliProbabilityDistribution(probability);
        this.inclusive = inclusive;
    }

    public override double Mean => 1.0 / this.bernoulli.Mean;

    public override double Variance => (1.0 - this.bernoulli.Mean) / Math.Pow(this.bernoulli.Mean, 2.0);

    public override double Density(int x)
    {
        if (x == 0)
        {
            return 0.0;
        }

        return
            inclusive ?
            this.bernoulli.Mean * Math.Pow(1.0 - this.bernoulli.Mean, x - 1) :
            this.bernoulli.Mean * Math.Pow(1.0 - this.bernoulli.Mean, x);
    }

    public override IEnumerator<int> GetEnumerator()
    {
        while (true)
        {
            yield return this.InverseDistribution(Uniform.NextDouble());
        }
    }

    private int InverseDistribution(double x)
    {
        // According to Smalltalk-80 p. 430, this method is from
        // Knuth, Vol2, pp. 116-117
        return (int)Math.Ceiling(Math.Log(x) / Math.Log(1.0 - this.bernoulli.Mean));
    }
}