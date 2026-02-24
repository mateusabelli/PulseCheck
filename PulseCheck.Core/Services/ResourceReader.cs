using PulseCheck.Core.Abstractions;
using PulseCheck.Core.Domain;

namespace PulseCheck.Core.Services;

public class ResourceReader(ICommandRunner runner, ICommandParser parser)
{
    public ResourceSnapshot ReadUsagePercent()
    {
        return new ResourceSnapshot(CpuUsagePercent(), MemoryUsagePercent());
    }

    private float MemoryUsagePercent()
    {
        var commandOutput = runner.Run("free -m | grep Mem | awk '{print $2, $3}'");
        var result = parser.ParseMemoryUsagePercent(commandOutput);
        return result;
    }

    private float CpuUsagePercent()
    {
        var commandOutput = runner.Run("top -b -n 1 | grep ^%Cpu | awk '{print $8}'");
        var result = parser.ParseCpuUsagePercent(commandOutput);
        return result;
    }
}