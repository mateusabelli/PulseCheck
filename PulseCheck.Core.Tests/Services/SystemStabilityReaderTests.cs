using PulseCheck.Core.Domain;
using PulseCheck.Core.Services;

namespace PulseCheck.Core.Tests.Services;

public class SystemStabilityReaderTests
{
    private readonly StabilityThresholds _thresholds = new(80f, 90f);

    [Theory]
    [InlineData(33.5f, 54.2f, StabilityState.Stable)] // Happy path
    [InlineData(98.4f, 12.5f, StabilityState.Unstable)] // High CPU
    [InlineData(22.4f, 95.8f, StabilityState.Unstable)] // High Memory
    [InlineData(85.0f, 95.0f, StabilityState.Unstable)] // Both High
    public void GetCurrentState_ShouldReturnExpectedState(float cpu, float mem, StabilityState expected)
    {
        var snapshot = new ResourceSnapshot(cpu, mem);

        var result = SystemStabilityReader.GetCurrentState(snapshot, _thresholds);

        Assert.Equal(expected, result);
    }
}