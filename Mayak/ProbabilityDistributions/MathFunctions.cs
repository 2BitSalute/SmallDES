namespace Mayak.ProbabilityDistributions;

public class MathFunctions
{
    public static int Factorial(int x)
    {
        int factorial = 1;
        while (x > 1)
        {
            factorial *= x;
            x--;
        }

        return factorial;
    }
}