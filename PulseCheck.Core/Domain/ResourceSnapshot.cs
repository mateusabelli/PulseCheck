namespace PulseCheck.Core.Domain;

public record ResourceSnapshot(
    float CpuUsagePercent,
    float MemoryUsagePercent
);