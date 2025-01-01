using Mayak.ProbabilityDistributions.Continuous;

namespace Mayak.Examples;

public class UniformExample : IExample
{
    public string Name => "Uniform";

    public void Run()
    {
        var dist = new UniformProbabilityDistribution(start: 0.0, end: TimeSpan.TicksPerDay);

        Console.WriteLine($"The probability of any given tick in a day having the event: {dist.Density(0)}");

        Console.Write("Samples:");

        using var samples = dist.GetEnumerator();

        for (int i = 0; i < 10; i++)
        {
            samples.MoveNext();

            Console.Write($" {TimeSpan.FromTicks((long)samples.Current)}");
        }

        Console.WriteLine();
    }
}
