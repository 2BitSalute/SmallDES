namespace Mayak.ProbabilityDistributions;

public interface IProbabilityDistribution<T> : IEnumerable<T>
{
    /// <summary>
    /// This is the density function.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    double Density(T x);

    double Distribution(IEnumerable<T> a);
}
