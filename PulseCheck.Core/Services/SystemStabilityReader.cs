using PulseCheck.Core.Abstractions;
using PulseCheck.Core.Domain;

namespace PulseCheck.Core.Services;

public static class SystemStabilityReader
{
    public static StabilityState GetCurrentState(ResourceSnapshot resourceSnapshot, StabilityThresholds thresholds)
    {
        var (cpuThreshold, memoryThreshold) = thresholds;
        var (cpuUsage, memoryUsage) = resourceSnapshot;

        return cpuUsage < cpuThreshold && memoryUsage < memoryThreshold
            ? StabilityState.Stable
            : StabilityState.Unstable;
    }
}