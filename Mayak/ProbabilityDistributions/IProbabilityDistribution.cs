namespace Mayak.ProbabilityDistributions;

public interface IProbabilityDistribution<T> : IEnumerable<T>
{
    /// <summary>
    /// This is the density function.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    double Density(T x);

    /// <summary>
    /// This is the cumulative distribution function. The argument is a range of contiguous values of the random
    /// variable. The distribution is mathenatically the area under the probability curve within the specified
    /// interval.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    double Distribution(IEnumerable<T> a);
}
