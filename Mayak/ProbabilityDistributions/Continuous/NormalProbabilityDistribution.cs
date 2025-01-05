namespace Mayak.ProbabilityDistributions.Continuous;

/// <summary>
/// AKA Gausssian distribution.
///
/// Useful in summarizing or approximating other distributions.
///
/// The approximation is accurate in the regions near the mean.
/// The errors in approximation increase towards the tails.
/// </summary>
public class NormalProbabilityDistribution : ContinuousProbabilityDistribution
{
    public override double Mean => throw new NotImplementedException();

    public override double Variance => throw new NotImplementedException();

    public override double Density(double x)
    {
        throw new NotImplementedException();
    }

    protected override double InverseDistribution(double x)
    {
        throw new NotImplementedException();
    }
}