
using System.ComponentModel;

namespace Mayak.ProbabilityDistributions.Discrete;

/// <summary>
/// Represents N repeated, independent Bermoulli distributions, where
///     N is >= 1.
///
/// It answers the question:
///     - How many successes are there in N trials?
/// </summary>
public class BinomialProbabilityDistribution : DiscreteProbabilityDistribution<int>
{
    private readonly int n;

    /// <summary>
    /// In Smalltalk-80, Binomial was a derived class of Bernoulli, but because
    ///     we have types and the type of x in Density is not the same between
    ///     the two classes, we need to compose rather than inherit.
    /// </summary>
    private readonly BernoulliProbabilityDistribution bernoulli;

    /// <summary>
    /// Binomial and Bernoulli are identical if N (numberOfEvents) is 1
    /// </summary>
    /// <param name="numberOfEvents">The number of trials</param>
    /// <param name="mean">The probability of success of each trial, added</param>
    public BinomialProbabilityDistribution(int numberOfEvents, double mean)
    {
        if (numberOfEvents <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfEvents), "Number of events must be > 0");
        }

        this.bernoulli = new BernoulliProbabilityDistribution(mean / numberOfEvents);
        this.n = numberOfEvents;
    }

    public override double Mean => this.n * this.bernoulli.Mean;

    public override double Variance => this.n * this.bernoulli.Variance;

    /// <summary>
    /// An adequate but slow method is to sample Bernoulli N times.
    /// </summary>
    /// <returns></returns>
    public override IEnumerator<int> GetEnumerator()
    {
        while (true)
        {
            using var dist = bernoulli.GetEnumerator();
            var countOfSuccesses = 0;
            for (int i = 0; i < this.n; i++)
            {
                dist.MoveNext();

                if (dist.Current)
                {
                    countOfSuccesses++;
                }
            }

            yield return countOfSuccesses;
        }
    }

    /// <summary>
    /// What is the probability that x successes will occur in the next N trials?
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public override double Density(int x)
    {
        if (x >= 0 && x < this.n)
        {
            return
                ComputeSampleOutOf(n, x) /
                ComputeSampleOutOf(x, x) *
                Math.Pow(this.bernoulli.Mean, x) *
                Math.Pow(1.0 - this.bernoulli.Mean, this.n - x);
        }
        else
        {
            return 0.0;
        }
    }
}