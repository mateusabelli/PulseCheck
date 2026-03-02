using PulseCheck.Core.Domain;
using PulseCheck.Core.Platform;
using PulseCheck.Core.Services;

var runner = new BashCommandRunner();
var parser = new BashOutputParser();
var thresholds = new StabilityThresholds(85.0f, 95.0f);

var resourceReader = new ResourceReader(runner, parser);

var snapshot = resourceReader.ReadUsagePercent();
var stability = SystemStabilityReader.GetCurrentState(snapshot, thresholds);

Console.WriteLine("In use system resources");
Console.WriteLine($"CPU: {snapshot.CpuUsagePercent}%");
Console.WriteLine($"MEM: {snapshot.MemoryUsagePercent}%");
Console.WriteLine($"The system is {stability}");