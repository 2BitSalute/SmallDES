
namespace Mayak.ProbabilityDistributions.Discrete;

/// <summary>
/// Useful in the case of a sample space of two possibilities.
/// In simulations, we use a Bernoulli distribution to tell us whether or not an event
/// occurs, for example, does a car arrive in the next secon, or will a machine
/// break down today.
///
/// In the context of simulations, it is useful for answering the question of:
///     - Will a success occur in the next trial?
/// </summary>
public class BernoulliProbabilityDistribution : DiscreteProbabilityDistribution<bool>
{
    private readonly double probabilityOfSuccess;

    public override double Mean => this.probabilityOfSuccess;

    public override double Variance => this.probabilityOfSuccess * (1.0 - this.probabilityOfSuccess);

    /// <summary>
    /// Probability of success and probability of failure (the complement of success).
    /// </summary>
    /// <param name="mean">
    /// The mean of the Bernoulli distribution.
    /// </param>
    public BernoulliProbabilityDistribution(double mean)
    {
        if (mean is < 0.0 or > 1.0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(mean),
                message: "Probability must be between 0.0 and 1.0");
        }

        this.probabilityOfSuccess = mean;
    }

    public override double Density(bool x) => x ? this.probabilityOfSuccess : (1.0 - this.probabilityOfSuccess);

    public override IEnumerator<bool> GetEnumerator()
    {
        while (true)
        {
            yield return Uniform.NextDouble() <= this.probabilityOfSuccess;
        }
    }
}