using Mayak.ProbabilityDistributions.Discrete;

namespace Mayak.Examples;

public class PoissonExample : IExample
{
    public string Name => "Poisson";

    public void Run()
    {
        // Average rate is, let's say, 3 customers arriving every 10 minutes
        var dist = new PoissonProbabilityDistribution(mean: 3);

        Console.WriteLine($"What is the probability of exactly 2 customers arriving in the next 10 minutes?\nDensity: {dist.Density(2)}");

        Console.Write("How many customers arrive in each 10 minute interval?\nSamples:");

        using var samples = dist.GetEnumerator();
        for (int i = 0; i < 100; i++)
        {
            samples.MoveNext();

            Console.Write($" {samples.Current}");
        }

        Console.WriteLine();
    }
}