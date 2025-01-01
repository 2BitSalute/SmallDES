using Mayak.ProbabilityDistributions.Continuous;

namespace Mayak.Examples;

public class ExponentialExample : IExample
{
    public string Name => "Exponential";

    public void Run()
    {
        // 3 customers every 10 minutes
        var dist = new ExponentialProbabilityDistribution(mean: 10.0 / 3);

        Console.WriteLine($"What is the probability of the next customer arriving in the next minute?\nDensity: {dist.Density(1.0)}");

        Console.Write("How much time to the next customer arriving?\nSamples:");

        using var samples = dist.GetEnumerator();
        for (int i = 0; i < 100; i++)
        {
            samples.MoveNext();

            Console.Write($" {samples.Current} minutes");
        }

        var total = 0.0;
        for (int i = 0; i < 1000000; i++)
        {
            samples.MoveNext();

            total += samples.Current;
        }

        Console.WriteLine();

        Console.WriteLine($"On average: {total / 1000000.0}");
    }
}
