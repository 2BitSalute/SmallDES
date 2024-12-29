namespace Mayak.ProbabilityDistributions;

public abstract class DiscreteProbabilityDistribution<T> : ProbabilityDistribution<T>
{
    public override double Distribution(IEnumerable<T> a)
    {
        return a.Select(this.Density).Sum();
    }
}