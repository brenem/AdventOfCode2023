namespace AdventOfCode2023;
#nullable disable

public class Day15
{
    public int Part1(string[] input)
    {
        var steps = input[0].Split(',');
        var totalHashSum = steps.Select(CalculateHashSum).Sum();
        return totalHashSum;
    }

    public int Part2(string[] input)
    {
        var steps = input[0].Split(',');
        var boxes = new Dictionary<int, Queue<(string Label, int FocalLength)>>();

        foreach (var step in steps)
        {
            var stepParts = step.Split('=', '-');
            var label = stepParts[0];
            var boxId = CalculateHashSum(label);

            Queue<(string Label, int FocalLength)> lensQueue;
            if (boxes.TryGetValue(boxId, out Queue<(string Label, int FocalLength)> value))
            {
                lensQueue = value;
            }
            else
            {
                lensQueue = new Queue<(string Label, int FocalLength)>();
                boxes.Add(boxId, lensQueue);
            }

            if (step.Contains('-'))
            {
                var lenses = lensQueue.ToList();
                var lensToRemove = lenses.Find(x => x.Label == label);
                if (lensToRemove != default)
                {
                    lenses.Remove(lensToRemove);

                    lensQueue.Clear();
                    lenses.ForEach(x => lensQueue.Enqueue(x));
                }
            }
            else
            {
                var focalLength = int.Parse(stepParts[1]);
                var lens = (label, focalLength);

                var lenses = lensQueue.ToList();
                var existingLensIndex = lenses.FindIndex(x => x.Label == label);
                if (existingLensIndex != -1)
                {
                    lenses[existingLensIndex] = lens;

                    lensQueue.Clear();
                    lenses.ForEach(x => lensQueue.Enqueue(x));
                }
                else
                {
                    lensQueue.Enqueue(lens);
                }
            }
        }

        var totalFocusingPower = 0;
        var boxesWithLenses = boxes.Where(x => x.Value.Count > 0);

        foreach (var box in boxesWithLenses)
        {
            var boxId = box.Key;
            var lenses = box.Value;

            var lensSlot = 1;
            while (lenses.TryDequeue(out (string Label, int FocalLength) lens))
            {
                var (_, focalLength) = lens;
                totalFocusingPower += (1 + boxId) * lensSlot * focalLength;

                lensSlot++;
            }
        }

        return totalFocusingPower;
    }

    public int CalculateHashSum(string input)
    {
        var hashSum = 0;

        foreach (var ch in input)
        {
            hashSum += ch;
            hashSum *= 17;
            hashSum %= 256;
        }

        return hashSum;
    }
}
