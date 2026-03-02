namespace PulseCheck.Core.Domain;

public readonly record struct StabilityThresholds(
    float CpuThreshold,
    float MemoryThreshold
);