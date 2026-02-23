using PulseCheck.App;

var cpuUsage = ResourceMonitor.GetCpuUsage();
var memoryUsage = ResourceMonitor.GetMemoryUsage();

Console.WriteLine("In use system resources");
Console.WriteLine($"CPU: {cpuUsage}%");
Console.WriteLine($"MEM: {memoryUsage}%");