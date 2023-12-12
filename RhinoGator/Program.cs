
// RhinoDevel, MT, 2023dec12

using Ele;

// F = 10000 Hz <=> T = 1/10000 s = 100000 ns

var clock = new Clock(
        new ClockParams
        {
            PulseSteps = 10000,
            StartHigh = false
        });

clock.Forward(0);
clock.Forward(9999);
clock.Forward(10000);
clock.Forward(19999);
clock.Forward(20000);

Console.WriteLine("RhinoGator!");