
// RhinoDevel, MT, 2023dec12

using Ele;

// F = 10000 Hz <=> T = 1/10000 s = 100000 ns

var clock = new Clock(
        new ClockParams
        {
            PulseSteps = 10000,
            StartHigh = false
        });
var highPass = new HighPass();

clock.Forward(0);
highPass.Update(clock.Output);
clock.Forward(9999);
highPass.Update(clock.Output);
clock.Forward(10000);
highPass.Update(clock.Output);
clock.Forward(10001);
highPass.Update(clock.Output);
clock.Forward(10002);
highPass.Update(clock.Output);
clock.Forward(19999);
highPass.Update(clock.Output);
clock.Forward(20000);
highPass.Update(clock.Output);

Console.WriteLine("RhinoGator!");