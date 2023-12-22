
// RhinoDevel, MT, 2023dec12

// F = 10000 Hz <=> T = 1/10000 s = 100000 ns

namespace RhinoGator
{
    internal class Program()
    {
        internal static void Main()
        {
            GameLoop.Start(new Examples.ExampleRsNandLatch());

            // var clock = new Ele.Basic.Clock.Clock(
            //         new Ele.Basic.Clock.ClockParams
            //         {
            //             PulseSteps = 10000,
            //             StartHigh = false
            //         });
            // var highPass = new Ele.Basic.HighPass();
            
            // clock.Update(new List<State>());
            // highPass.Update(new List<State>{clock.Output});
            // clock.Forward(9999 - 1);
            // clock.Update(new List<State>());
            // highPass.Update(new List<State>{clock.Output});

            // clock.Update(new List<State>());
            // highPass.Update(new List<State>{clock.Output});

            // clock.Update(new List<State>());
            // highPass.Update(new List<State>{clock.Output});

            // clock.Update(new List<State>());
            // highPass.Update(new List<State>{clock.Output});
            // clock.Forward(19999 - 10003);
            // clock.Update(new List<State>());
            // highPass.Update(new List<State>{clock.Output});

            // clock.Update(new List<State>());
            // highPass.Update(new List<State>{clock.Output});

            // var xor = new Ele.Assembled.Xor();

            // var inputs = new List<Tuple<State, State>>
            // {
            //     new Tuple<State, State>(State.Low, State.Low),
            //     new Tuple<State, State>(State.Low, State.Low),
            //     new Tuple<State, State>(State.Low, State.Low),
            //     new Tuple<State, State>(State.Low, State.Low),
            //     new Tuple<State, State>(State.Low, State.Low),
            //     new Tuple<State, State>(State.Low, State.Low),

            //     new Tuple<State, State>(State.Low, State.Rising),

            //     new Tuple<State, State>(State.Low, State.High),
            //     new Tuple<State, State>(State.Low, State.High),
            //     new Tuple<State, State>(State.Low, State.High),
            //     new Tuple<State, State>(State.Low, State.High),
            //     new Tuple<State, State>(State.Low, State.High),
            //     new Tuple<State, State>(State.Low, State.High),

            //     new Tuple<State, State>(State.Rising, State.Falling),

            //     new Tuple<State, State>(State.High, State.Low),
            //     new Tuple<State, State>(State.High, State.Low),
            //     new Tuple<State, State>(State.High, State.Low),
            //     new Tuple<State, State>(State.High, State.Low),
            //     new Tuple<State, State>(State.High, State.Low),
            //     new Tuple<State, State>(State.High, State.Low),

            //     new Tuple<State, State>(State.High, State.Rising),
                
            //     new Tuple<State, State>(State.High, State.High),
            //     new Tuple<State, State>(State.High, State.High),
            //     new Tuple<State, State>(State.High, State.High),
            //     new Tuple<State, State>(State.High, State.High),
            //     new Tuple<State, State>(State.High, State.High),
            //     new Tuple<State, State>(State.High, State.High)
            // };

            // Console.WriteLine($"Initial XOR output is: {xor.Output}");
            // for(int i = 0; i < inputs.Count; ++i)
            // {
            //     var t = inputs[i];

            //     xor.Update(t.Item1, t.Item2);

            //     Console.WriteLine(
            //         $"Index {i}: {t.Item1} XOR {t.Item2} = {xor.Output}");
            // }
        }
    }
}