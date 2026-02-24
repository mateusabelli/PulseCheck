using PulseCheck.Core.Platform;
using PulseCheck.Core.Services;

var runner = new BashCommandRunner();
var parser = new BashCommandOutputParser();

var resourceReader = new ResourceReader(runner, parser);

var (cpuUsage, memoryUsage) = resourceReader.ReadUsagePercent();

Console.WriteLine("In use system resources");
Console.WriteLine($"CPU: {cpuUsage}%");
Console.WriteLine($"MEM: {memoryUsage}%");