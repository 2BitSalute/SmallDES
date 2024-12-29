namespace Mayak.ProbabilityDistributions;

public abstract class ContinuousProbabilityDistribution : ProbabilityDistribution<double>
{
    /// <summary>
    /// This is a slow and dirty trapezoidal integration to determine the area under
    /// the probability function curve y=density(x) for x in the specified colleciton.
    /// The method assumes that the collection contains numerically-ordered elements.
    /// </summary>
    /// <param name="a">Numerically ordered elements (x).</param>
    /// <returns>The probability distribution over the range in `a`</returns>
    public override double Distribution(IEnumerable<double> a)
    {
        double total = 0.0;

        using var enumerator = a.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            return total;
        }

        double x2 = enumerator.Current;  // Get the first element in the collection
        double y2 = this.Density(x2);  // Compute the density for the first element

        while (enumerator.MoveNext())
        {
            double x1 = x2;
            x2 = enumerator.Current;
            double y1 = y2;
            y2 = this.Density(x2);

            // Apply the trapezoidal rule for area calculation
            total += (x2 - x1) * (y2 + y1);
        }

        // Complete the trapezoidal integration
        return total * 0.5;
    }
}
