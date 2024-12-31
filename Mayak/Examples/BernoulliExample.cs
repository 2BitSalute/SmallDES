using Mayak.ProbabilityDistributions.Discrete;

namespace Mayak.Examples;

public class BernoulliExample : IExample
{
    public string Name => "Bernoulli";

    public void Run()
    {
        // Note: 4 / 52 results in 0!
        var dist = new BernoulliProbabilityDistribution(mean: 4.0 / 52.0);

        var counter = 0;
        foreach (var sample in dist)
        {
            counter++;
            if (sample)
            {
                Console.WriteLine($"took {counter} trials to draw a 4 out of a deck of cards (with replacement)");
                break;
            }
        }
    }
}