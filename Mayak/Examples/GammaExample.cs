using Mayak.ProbabilityDistributions.Continuous;

namespace Mayak.Examples;

public class GammaExample : IExample
{
    public string Name => "Gamma";

    public void Run()
    {
        // 3 customers every 10 minutes
        // In the exponential distribution, the mean is the average time between arrival of each customer,
        // while in the gamma distribution, it is the average time between arrival of each k customers (k = # of events)
        // We want to know how long before the 6th customer arrives
        var dist = new GammaProbabilityDistribution(events: 6, mean: 6 * (10.0 / 3));

        Console.WriteLine($"What is the probability of the 6th customer arriving in the next 11 minutes?\nDensity: {dist.Density(11.0)}");

        Console.WriteLine($"TWhat is the probability of 6 customers arriving up to 22 minutes?\nCDF: {dist.Distribution([0.0, 22.0])}");

        Console.Write("How much time to the next 6 customers arriving?\nSamples:");

        using var samples = dist.GetEnumerator();
        for (int i = 0; i < 10; i++)
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