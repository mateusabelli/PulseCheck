using System.Globalization;
using PulseCheck.Core.Abstractions;

namespace PulseCheck.Core.Platform;

public class BashOutputParser : ICommandParser
{
    public float ParseCpuUsagePercent(string stdout)
    {
        var canParseResult = float.TryParse(stdout.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture,
            out var result);
        if (!canParseResult)
            throw new InvalidOperationException($"Could not parse CPU usage from command output {stdout}");

        var usage = MathF.Round(100f - result, 2);
        return usage;
    }

    public float ParseMemoryUsagePercent(string stdout)
    {
        var stdoutArray = stdout.Split(" ");

        if (stdoutArray.Length != 2)
            throw new InvalidOperationException($"Could not parse values from command output {stdout}");

        var canParseTotalMemory = int.TryParse(stdoutArray[0], out var totalResult);
        if (!canParseTotalMemory)
            throw new InvalidOperationException($"Could not parse total memory value from command output {stdout}");

        var canParseInUseMemory = int.TryParse(stdoutArray[1], out var inUseResult);
        if (!canParseInUseMemory)
            throw new InvalidOperationException($"Could not parse in use memory from command output {stdout}");

        var totalMemory = totalResult / 1024f;
        var inUseMemory = inUseResult / 1024f;

        return MathF.Round(inUseMemory * 100f / totalMemory, 2);
    }
}