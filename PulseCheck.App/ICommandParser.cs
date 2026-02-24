namespace PulseCheck.App;

public interface ICommandParser
{
    float ParseCpuUsagePercent(string stdout);
    float ParseMemoryUsagePercent(string stdout);
}