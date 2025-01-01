using System.Xml.Serialization;

namespace Mayak.ProbabilityDistributions.Continuous;

/// <summary>
/// The underlying events are Poisson-distributed.
///
/// Answers the question:
///     - How long before the first/next event occurs?
///
/// A more useful distribution than the Poisson for discrete event simulations.
/// </summary>
public class ExponentialProbabilityDistribution : ContinuousProbabilityDistribution
{
    private readonly double μ;

    /// <summary>
    /// The mean here represents the mean number of occurrences per unit of time.
    /// The derived parameter is the reciprocal, and represents the mean time between events.
    /// </summary>
    /// <param name="mean"></param>
    public ExponentialProbabilityDistribution(double mean)
    {
        // Since teh exponential parameter is the same as Poisson's, if we are given
        // the mean of the exponentiall, we take reciprocal to get the probability parameter.
        if (mean == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mean), "The mean must be > 0.0");
        }

        this.μ = 1.0 / mean;
    }

    public double Mean => 1.0 / this.μ;

    public double Variance => 1.0 / Math.Pow(this.μ, 2);

    /// <summary>
    ///
    /// </summary>
    /// <param name="a">Should be an interval, not a collection</param>
    /// <returns></returns>
    public override double Distribution(IEnumerable<double> a)
    {
        // TODO: should we have a type parameter for Distribution's param, too?
        // What are we really inheriting from ContinousProbability here?
        // Nothing, actually.
        // This design is not very nice. Where's my Liskov Substitution Principle?
        var aList = a.ToArray();
        if (aList.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(a), "Must have at least 2 elements");
        }

        var start = aList[0];
        var end = aList[^1];

        if (end <= 0.0)
        {
            return 0.0;
        }

        return
            1.0 - Math.Exp(-(this.μ * end)) -
                (start > 0 ? this.Distribution([0.0, start]) : 0.0);
    }

    /// <summary>
    /// The probability that the next event will occur in th enext time interval.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public override double Density(double x)
    {
        if (x > 0.0)
        {
            return this.μ * Math.Exp(-(this.μ * x));
        }

        return 0.0;
    }

    protected override double InverseDistribution(double x)
    {
        // Implementation according to Knuth, Vol. 2, p. 114
        return (-Math.Log(x)) / this.μ;
    }
}