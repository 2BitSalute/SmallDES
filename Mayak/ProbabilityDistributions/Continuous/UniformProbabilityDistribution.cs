namespace Mayak.ProbabilityDistributions.Continuous;

/// <summary>
/// Answers the question:
///     - Given a set of equally likely events, which one occurs?
/// </summary>
public class UniformProbabilityDistribution : ContinuousProbabilityDistribution
{
    private readonly double start;
    private readonly double end;

    public UniformProbabilityDistribution(double start, double end)
    {
        if (start > end)
        {
            throw new ArgumentOutOfRangeException(nameof(end), "The end point must be >= than the start");
        }

        this.start = start;
        this.end = end;
    }

    public double Mean => (this.start + this.end) / 2;

    public double Variance => Math.Pow(this.end - this.start, 2) / 12;

    public override double Density(double x)
    {
        if (x >= this.start && x <= this.end)
        {
            return 1.0 / (this.end - this.start);
        }
        else
        {
            return 0.0;
        }
    }

    protected override double InverseDistribution(double x)
    {
        // A random point within the interval
        return this.start + (x * (this.end - this.start));
    }
}