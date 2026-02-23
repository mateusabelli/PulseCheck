using System.Diagnostics;

namespace PulseCheck.App;

public static class ResourceMonitor
{
    public static float GetMemoryUsage()
    {
        var cmd = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = "-c \"free -m | grep Mem | awk '{print $2, $3}'\"",
            RedirectStandardOutput = true,
        };

        using var process = Process.Start(cmd);
        if (process == null)
            throw new InvalidOperationException("Could not start process");

        process.WaitForExit();

        var stdout = process.StandardOutput.ReadToEnd().Split(" ");

        var canParseTotalMemory = int.TryParse(stdout[0], out var totalResult);
        if (!canParseTotalMemory)
            throw new InvalidOperationException("Could not parse total memory value from command");

        var canParseInUseMemory = int.TryParse(stdout[1], out var inUseResult);
        if (!canParseInUseMemory)
            throw new InvalidOperationException("Could not parse in use memory from command");

        var totalMemory = totalResult / 1024f;
        var inUseMemory = inUseResult / 1024f;

        var usage = MathF.Round(inUseMemory * 100f / totalMemory, 2);
        return usage;
    }

    public static float GetCpuUsage()
    {
        var cmd = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = "-c \"top -b -n 1 | grep ^%Cpu | awk '{print $8}'\"",
            RedirectStandardOutput = true,
        };

        using var process = Process.Start(cmd);
        if (process == null)
            throw new InvalidOperationException("Could not start the process");

        process.WaitForExit();

        var stdout = process.StandardOutput.ReadToEnd();

        var canParseResult = float.TryParse(stdout, out var result);
        if (!canParseResult)
            throw new InvalidOperationException("Could not parse cpu usage from command");

        var usage = MathF.Round(100f - result, 2);
        return usage;
    }
}