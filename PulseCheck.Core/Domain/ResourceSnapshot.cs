namespace PulseCheck.Core.Domain;

public readonly record struct ResourceSnapshot(
    float CpuUsagePercent,
    float MemoryUsagePercent
);