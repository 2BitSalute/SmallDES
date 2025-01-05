using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

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
    private readonly double sigma;
    private readonly double mean;

    /// <summary>
    ///
    /// </summary>
    /// <param name="mean">AKA mu/μ</param>
    /// <param name="deviation">AKA sigma</param>
    public NormalProbabilityDistribution(double mean, double deviation)
    {
        if (deviation <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(deviation), "Standard deviation must be > 0.0");
        }

        this.mean = mean;
        this.sigma = deviation;
    }

    public override double Mean => this.mean;

    public override double Variance => Math.Pow(this.sigma, 2.0);

    public override IEnumerator<double> GetEnumerator()
    {
        // Generates a random double (-1, 1)
        static double NextDouble()
        {
            var next = Uniform.NextDouble();
            return Uniform.NextDouble() < 0.5 ? next : next * -1.0;
        }

        while (true)
        {
            // Polar method for normal derivatives. Knuth vol 2, pp. 104, 113
            double v1 = 0.0, v2;
            var s = 1.0;
            while (s >= 1.0)
            {
                v1 = NextDouble();
                v2 = NextDouble();
                s = Math.Pow(v1, 2.0) + Math.Pow(v2, 2.0);
            }

            var u = Math.Sqrt(-2.0 * Math.Log(s) / s);

            yield return this.mean + this.sigma * v1 * u;
        }
    }

    public override double Density(double x)
    {
        return Math.Exp(-0.5 * Math.Pow((x - this.mean) / this.sigma, 2.0)) / (this.sigma * Math.Sqrt(2 * Math.PI));
    }
}