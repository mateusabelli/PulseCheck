using PulseCheck.Core.Abstractions;
using PulseCheck.Core.Domain;
using PulseCheck.Core.Services;

namespace PulseCheck.Cli;

public class App(ICommandRunner runner, ICommandParser parser)
{
    private readonly StabilityThresholds _thresholds = new(85.0f, 95.0f);
    private readonly ResourceReader _resourceReader = new(runner, parser);

    public void Run()
    {
        var snapshot = _resourceReader.ReadUsagePercent();
        var stability = SystemStabilityReader.GetCurrentState(snapshot, _thresholds);

        Console.WriteLine("In use system resources");
        Console.WriteLine($"CPU: {snapshot.CpuUsagePercent}%");
        Console.WriteLine($"MEM: {snapshot.MemoryUsagePercent}%");
        Console.WriteLine($"The system is {stability}");
    }
}