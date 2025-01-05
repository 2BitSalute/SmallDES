using System.Runtime.ConstrainedExecution;

namespace Mayak.ProbabilityDistributions.Continuous;

/// <summary>
/// Answers the question:
///     - How long before the Nth event occurs?
/// </summary>
public class GammaProbabilityDistribution : ExponentialProbabilityDistribution
{
    private readonly int events;

    /// <summary>
    /// </summary>
    /// <param name="events">AKA `k`</param>
    /// <param name="mean">Has to be the average time between k arrivals</param>
    public GammaProbabilityDistribution(int events, double mean) : base(mean / events)
    {
        if (events <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(events), "The number of events must be > 0");
        }

        this.events = events;
    }

    public override double Mean => base.Mean * this.events;

    public override double Variance => base.Variance * this.events;

    /// <summary>
    /// Represents the probability the time to the k'th event will be equal to parameter
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public override double Density(double time)
    {
        if (time <= 0)
        {
            return 0.0;
        }

        // AKA k or alpha - the number of events we're waiting for
        var shape = this.events;

        // For positive integers, gamma(shape) is this:
        var gamma = MathFunctions.Factorial(shape - 1);

        // theta - the time between arrivals
        // Rate is the reciprocal of theta
        var rate = 1 / base.Mean;

        // OK, I'm getting very confused with all the variables now.
        // TODO: return to Gamma to figure out what it's all about.
        var firstTerm = Math.Pow(rate, shape); // OK
        var secondTerm = Math.Pow(time, shape - 1); // OK
        var thirdTerm = Math.Exp(-(time * rate));

        return firstTerm * secondTerm * thirdTerm / gamma;
    }

    public override IEnumerator<double> GetEnumerator()
    {
        while (true)
        {
            var samples = base.GetEnumerator();
            var sample = 0.0;
            for (int i = 0; i < this.events; i++)
            {
                samples.MoveNext();
                sample += samples.Current;
            }

            yield return sample;
        }
    }

    // This could be complete garbage
    public override double Distribution(IEnumerable<double> a)
    {
        var aList = a.ToArray();
        if (aList.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(a), "Must have at least 2 elements");
        }

        var start = aList[0];
        var end = aList[^1];

        if (start < 0 || end <= start)
        {
            return 0.0;
        }

        // Rate is AKA lambda
        // base.Mean is AKA theta (rate of arrival of individual events)
        double rate = 1.0 / base.Mean;

        double sum = 0.0;
        for (int n = 0; n < this.events; n++)
        {
            sum += Math.Pow(end * rate, n) / MathFunctions.Factorial(n);
        }

        return
            1.0 -
            Math.Exp(-(end * rate)) * sum -
            this.Distribution([0.0, start]);
    }
}