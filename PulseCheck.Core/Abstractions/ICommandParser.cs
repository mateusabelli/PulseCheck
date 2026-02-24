namespace PulseCheck.Core.Abstractions;

public interface ICommandParser
{
    float ParseCpuUsagePercent(string stdout);
    float ParseMemoryUsagePercent(string stdout);
}