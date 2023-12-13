using System.Reflection;

namespace AdventOfCode2023;

public static class Extensions
{
    public static async Task<object?> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
    {
        if (@this.Invoke(obj, parameters) is Task task)
        {
            await task!.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty!.GetValue(task);
        }
        return null;
    }
}
