using Mayak.ProbabilityDistributions.Continuous;

namespace Mayak.Examples;

public class NormalExample : IExample
{
    public string Name => "Normal";

    public void Run()
    {
        // Average adult male height in centimeters
        var dist = new NormalProbabilityDistribution(mean: 175, deviation: 10);

        var exactly190 = dist.Density(190);

        Console.WriteLine($"The probability of being exactly 190 cm is {exactly190}");

        // Let's make that quick and dirty trapezoidal integration work
        var range = new List<double>();
        for (double h = 0.0; h <= 190.0; h++)
        {
            range.Add(h);
        }

        double tallerThan190 = 1.0 - dist.Distribution(range);
        Console.WriteLine($"The probability of being taller than 190 cm is {tallerThan190}");

        Console.Write("How tall is the next adult male?\nSamples:");

        using var samples = dist.GetEnumerator();
        for (int i = 0; i < 10; i++)
        {
            samples.MoveNext();

            Console.Write($" {samples.Current}");
        }
    }
}