using Mayak.ProbabilityDistributions.Discrete;

namespace Mayak.Examples;

public class BinomialExcample : IExample
{
    public string Name => "Binomial";

    public void Run()
    {
        var sample = new BinomialProbabilityDistribution(
            numberOfEvents: 5,
            mean: 2.5);

        using var dist = sample.GetEnumerator();

        dist.MoveNext();

        Console.WriteLine($"How many heads did we get in 5 trials?\nSample: {dist.Current}");
        Console.WriteLine($"What is the probability of getting heads 3 times in 5 trials?\nDensity: {sample.Density(3)}");
    }
}