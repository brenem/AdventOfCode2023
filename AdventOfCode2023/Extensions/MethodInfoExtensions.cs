using System.Reflection;

namespace AdventOfCode2023.Extensions;

public static class MethodInfoExtensions
{
    public static async Task<object?> InvokeAsync(this MethodInfo mi, object obj, params object[] parameters)
    {
        if (mi.Invoke(obj, parameters) is Task task)
        {
            await task!.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty!.GetValue(task);
        }
        return null;
    }
}
