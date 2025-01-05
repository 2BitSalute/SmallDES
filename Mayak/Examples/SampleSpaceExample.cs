using Mayak.ProbabilityDistributions.Discrete;

namespace Mayak.Examples;

public class SampleSpaceExample : IExample
{
    public string Name => "Sample Space";

    public void Run()
    {
        // p. 423
        var studentHeightFrequency = new List<(int Height, int NumStudents)>
        {
            ( 60, 3 ),
            ( 62, 2 ),
            ( 64, 4 ),
            ( 66, 3 ),
            ( 68, 5 ),
            ( 70, 3 )
        };

        // What is the probability of randomly selecting a student who is
        // 5'4" tall?

        var totalStudents = studentHeightFrequency.Aggregate(0, (total, el) => total + el.NumStudents);

        var densityFunction = studentHeightFrequency.Select(el => (el.Height, 1.0 * el.NumStudents / totalStudents));

        foreach (var el in densityFunction)
        {
            Console.WriteLine($"{el.Height}: {el.Item2}");
        }

        var data = new List<double>
        {
            60, 60, 60,
            62, 62,
            64, 64, 64, 64,
            66, 66, 66,
            68, 68, 68, 68, 68,
            70, 70, 70
        };

        var dist = new SampleSpaceProbabilityDistribution(data);

        var density64 = dist.Density(64);

        Console.WriteLine($"Probability of randomly selecting a student of height 64'' is {density64}");

        // Note that even though there aren't values for 61 or 63 in the sample,
        // the calculation is still accurate because we're simply summing probabilities,
        // and adding 0 for 61 and 63 is harmless.
        var density60to64 = dist.Distribution([60, 61, 62, 63, 64]);

        Console.WriteLine($"Probability of randomly selecting a student of height between 60 and 64'' is {density60to64}");

        Console.Write("Samples: ");

        using var enumerator = dist.GetEnumerator();
        for (int i = 0; i < 30; i++)
        {
            enumerator.MoveNext();
            Console.Write($"{enumerator.Current} ");
        }

        Console.WriteLine();
    }
}