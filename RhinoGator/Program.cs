
// RhinoDevel, MT, 2023dec12

// F = 10000 Hz <=> T = 1/10000 s = 100000 ns

namespace RhinoGator
{
    internal class Program()
    {
        internal static void Main()
        {
            GameLoop.Start(new Examples.ExampleClock());
        }
    }
}