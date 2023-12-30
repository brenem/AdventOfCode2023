using AdventOfCode2023.Extensions;

namespace AdventOfCode2023;
#nullable disable

public class Day20
{
    private const string FLIP_FLOP = "%";
    private const string CONJUCTION = "&";
    private const string BROADCASTER = "broadcaster";

    public long Part1(string[] input)
    {
        var modules = new Dictionary<string, Module>();

        foreach (var line in input)
        {
            var module = new Module(line);
            modules[module.Name] = module;
        }

        foreach (var (name, module) in modules)
        {
            foreach (var destination in module.Destinations)
            {
                if (modules.ContainsKey(destination) && modules[destination].Type == CONJUCTION)
                {
                    modules[destination].Memory[name] = PulseStrength.Low;
                }
            }
        }

        int lowPulses = 0;
        int highPulses = 0;

        int iterations = 1000;
        for (int i = 0; i < iterations; i++)
        {
            lowPulses++;

            var queue = new Queue<(string Module, string Destination, PulseStrength Pulse)>(
                modules[BROADCASTER].Destinations.Select(x => (BROADCASTER, x, PulseStrength.Low)));

            while (queue.Count > 0)
            {
                var (source, destination, pulse) = queue.Dequeue();

                Console.WriteLine($"{source} -{pulse}-> {destination}");

                if (pulse == PulseStrength.Low)
                    lowPulses++;
                else
                    highPulses++;

                if (!modules.ContainsKey(destination))
                    continue;

                var module = modules[destination];

                PulseStrength outgoingPulse;

                if (module.Type == FLIP_FLOP)
                {
                    if (pulse == PulseStrength.Low)
                    {
                        module.On = !module.On;
                        outgoingPulse = module.On ? PulseStrength.High : PulseStrength.Low;

                        foreach (var dest in module.Destinations)
                        {
                            queue.Enqueue((module.Name, dest, outgoingPulse));
                        }
                    }
                }
                else
                {
                    module.Memory[source] = pulse;

                    outgoingPulse = PulseStrength.High;

                    if (module.Memory.All(x => x.Value == PulseStrength.High))
                        outgoingPulse = PulseStrength.Low;

                    foreach (var dest in module.Destinations)
                    {
                        queue.Enqueue((module.Name, dest, outgoingPulse));
                    }
                }
            }
        }

        return lowPulses * highPulses;
    }

    public long Part2(string[] input)
    {
        var modules = new Dictionary<string, Module>();
        var rxFeeder = string.Empty;

        foreach (var line in input)
        {
            var module = new Module(line);
            modules[module.Name] = module;

            if (module.Destinations.Contains("rx"))
                rxFeeder = module.Name;
        }

        foreach (var (name, module) in modules)
        {
            foreach (var destination in module.Destinations)
            {
                if (modules.ContainsKey(destination) && modules[destination].Type == CONJUCTION)
                {
                    modules[destination].Memory[name] = PulseStrength.Low;
                }
            }
        }

        var lengths = new Dictionary<string, long>();
        var visited = modules.Where(x => x.Value.Destinations.Contains(rxFeeder)).ToDictionary(x => x.Key, x => false);
        long count = 0;

        while (true)
        {
            count++;

            var queue = new Queue<(string Module, string Destination, PulseStrength Pulse)>(
                modules[BROADCASTER].Destinations.Select(x => (BROADCASTER, x, PulseStrength.Low)));

            while (queue.Count > 0)
            {
                var (source, destination, pulse) = queue.Dequeue();

                Console.WriteLine($"{source} -{pulse}-> {destination}");

                if (!modules.ContainsKey(destination))
                    continue;

                var module = modules[destination];

                if (module.Name == rxFeeder && pulse == PulseStrength.High)
                {
                    visited[source] = true;

                    if (!lengths.ContainsKey(source))
                        lengths[source] = count;

                    if (visited.All(x => x.Value))
                    {
                        long product = 1;
                        foreach (var length in lengths.Values)
                        {
                            product = product * length / GCD(product, length);
                        }

                        return product;
                    }
                }

                PulseStrength outgoingPulse;

                if (module.Type == FLIP_FLOP)
                {
                    if (pulse == PulseStrength.Low)
                    {
                        module.On = !module.On;
                        outgoingPulse = module.On ? PulseStrength.High : PulseStrength.Low;

                        foreach (var dest in module.Destinations)
                        {
                            queue.Enqueue((module.Name, dest, outgoingPulse));
                        }
                    }
                }
                else
                {
                    module.Memory[source] = pulse;

                    outgoingPulse = PulseStrength.High;

                    if (module.Memory.All(x => x.Value == PulseStrength.High))
                        outgoingPulse = PulseStrength.Low;

                    foreach (var dest in module.Destinations)
                    {
                        queue.Enqueue((module.Name, dest, outgoingPulse));
                    }
                }
            }
        }

        return 0;
    }

    private long GCD(long a, long b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    class Module
    {
        public Module(string line)
        {
            var parts = line.SplitTrim(" -> ");

            var name = parts[0];

            if (name == BROADCASTER)
            {
                Name = name;
                Type = BROADCASTER;
            }
            else
            {
                Name = name[1..];
                Type = name[0].ToString();
            }

            Destinations = parts[1].SplitTrim(",");
        }

        public string Name { get; }
        public string Type { get; }
        public IEnumerable<string> Destinations { get; }
        public bool On { get; set; } = false;
        public Dictionary<string, PulseStrength> Memory { get; } = [];

        public override string ToString()
        {
            return $"type={Type}, destinations={string.Join(',', Destinations)}, memory={string.Join(',', Memory.Select(x => $"{x.Key}={x.Value}"))}, on={On}";
        }
    }

    enum PulseStrength
    {
        Low,
        High
    }
}
