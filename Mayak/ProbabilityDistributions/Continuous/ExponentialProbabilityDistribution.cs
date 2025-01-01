namespace Mayak.ProbabilityDistributions.Continuous;

/// <summary>
/// The underlying events are Poisson-distributed.
///
/// Answers the question:
///     - How long before the first/next event occurs?
/// </summary>
public class ExponentialProbabilityDistribution : ContinuousProbabilityDistribution
{
    public override double Density(double x)
    {
        throw new NotImplementedException();
    }

    protected override double InverseDistribution(double x)
    {
        throw new NotImplementedException();
    }
}