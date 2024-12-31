using Mayak.ProbabilityDistributions.Discrete;

namespace Mayak.Examples;

public class GeometricExample : IExample
{
    public string Name => "Geometric";

    public void Run()
    {
        //  Suppose 2 cars arrive at a ferry landing every minute, on average
        // NOTE: the original example is incorrect, see the errata for page 430
        // https://archive.computerhistory.org/resources/access/text/2024/06/102739373-05-0001-acc.pdf
        var sample = new GeometricProbabilityDistribution(mean: 60.0 / 2.0);

        // The density function can be used to answer the question:
        //  -What is the probability that it will take N trials before the next success?
        // In other words:
        //  - What is the probability that it will take 30 seconds before the next car arrives?
        Console.WriteLine($"What is the probability that it will take 30 seconds before the next car arrives?\nDensity: {sample.Density(30)}");

        Console.WriteLine($"Did the next car arrive in 30 to 40 seconds?\nCumulative distribution: {sample.Distribution([30, 40])}");
    }
}