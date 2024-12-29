namespace Mayak.ProbabilityDistributions;

using System.Collections;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// In SmallTalk, ProbabilityDistribution inherits from Stream,
/// which is like an IEnumerable or an F# sequence
/// </remarks>
public abstract class ProbabilityDistribution<T> : IProbabilityDistribution<T>
{
    /// <summary>
    /// Uniformly distributed random numbers in the range [0, 1)
    /// </summary>
    /// <remarks>
    /// In the original implementation, it's [0, 1], but it's more work in .NET
    /// </remarks>
    private static readonly Random Uniform = new ();

    /// <summary>
    /// The equivalent of `next`
    /// 
    /// This is a general random number generation method for any probability law;
    /// use the [0, 1) uniformly distributed random variable Uniform as the value
    /// of the law's distribution function. Obtain the next random value and then
    /// solve for the inverse. The inverse solution is defined by the subclass.
    /// </summary>
    /// <returns>The next random sample</returns>
    public IEnumerator<T> GetEnumerator()
    {
        while(true)
        {
            yield return this.InverseDistribution(Uniform.NextDouble());
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Subclass responsibility
    /// </summary>
    /// <param name="x"></param>
    /// <returns>The solution for the inverse distribution</returns>
    protected abstract T InverseDistribution(double x);

    /// <summary>
    /// Compute the number of ways one can draw a sample without
    /// replacement of size m from a set of size n.
    /// </summary>
    /// <param name="n">The size of the set.</param>
    /// <param name="m">The size of the sample.</param>
    /// <returns>The combination.</returns>
    protected static double ComputeSampleOutOf(int n, int m)
    {
        if (m > n)
        {
            return 0;
        }

        static int Factorial (int x)
        {
            int factorial = x;
            while (x > 1)
            {
                x--;
                factorial *= x;
            }

            return factorial;
        }

        return Factorial(n) / Factorial(n - m);
    }

    /// <inheritdoc />
    public abstract double Density(T x);

    /// <inheritdoc />
    public abstract double Distribution(IEnumerable<T> a);
}