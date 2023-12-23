using AdventOfCode2023.Models;

namespace AdventOfCode2023.Extensions.Extensions;

public static class GridExtensions
{
    public static T? FindGridNode<T>(this GridLocation location, IEnumerable<T> nodes) where T : GridNode
    {
        return nodes.SingleOrDefault(x => x.Location == location);
    }
}
