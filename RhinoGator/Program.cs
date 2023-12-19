
// RhinoDevel, MT, 2023dec12

// F = 10000 Hz <=> T = 1/10000 s = 100000 ns

namespace RhinoGator
{
    public class Program()
    {
        public static void Main()
        {
            var clock = new Ele.Basic.Clock.Clock(
                    new Ele.Basic.Clock.ClockParams
                    {
                        PulseSteps = 10000,
                        StartHigh = false
                    });
            var highPass = new Ele.Basic.HighPass();

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
        }
    }
}