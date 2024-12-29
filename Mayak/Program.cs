// See https://aka.ms/new-console-template for more information


using System.Reflection;
using Mayak.Examples;

// Get the assembly where the code is running
var assembly = Assembly.GetExecutingAssembly();

// Find all types that implement IExample
var typesImplementingIExample = assembly.GetTypes()
                                            .Where(t => typeof(IExample).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                                            .ToList();

// Instantiate each type and call DoSomething
foreach (var type in typesImplementingIExample)
{
    IExample? instance = (IExample?)Activator.CreateInstance(type);

    if (instance != null)
    {
        Console.WriteLine($"*** {instance.Name} ***");
        instance.Run();
    }
}