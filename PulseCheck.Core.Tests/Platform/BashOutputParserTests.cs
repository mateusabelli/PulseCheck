using PulseCheck.Core.Platform;

namespace PulseCheck.Core.Tests;

public class BashOutputParserTests
{
    private readonly BashOutputParser _parser = new();

    [Theory]
    [InlineData("  2.3 ", 97.70f)]
    [InlineData("88.5", 11.5f)]
    [InlineData("23.45", 76.55f)]
    [InlineData("100", 0.00f)]
    [InlineData("0", 100f)]
    public void ParseCpuUsagePercent_ShouldReturnPercent(string stdout, float expected)
    {
        var result = _parser.ParseCpuUsagePercent(stdout);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("abc")]
    [InlineData("12.3  extra")]
    public void ParseCpuUsagePercent_InvalidInput_ThrowsException(string input)
    {
        var exception = Assert.Throws<InvalidOperationException>(() => _parser.ParseCpuUsagePercent(input));
        Assert.Contains("Could not parse CPU usage", exception.Message);
    }

    [Theory]
    [InlineData("16384 8192", 50.00f)]
    [InlineData("32000 24576", 76.80f)]
    [InlineData("15872 3174", 20.00f)]
    [InlineData("8192 0", 0.00f)]
    [InlineData("4096 4096", 100.00f)]
    public void ParseMemoryUsagePercent_ShouldReturnPercent(string stdout, float expected)
    {
        var result = _parser.ParseMemoryUsagePercent(stdout);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("8192")]
    [InlineData("8192  ")]
    [InlineData("8192 abc")]
    [InlineData("8192 4500 extra")]
    public void ParseMemoryUsagePercent_InvalidInput_ThrowsException(string input)
    {
        var exception = Assert.Throws<InvalidOperationException>(() => _parser.ParseMemoryUsagePercent(input));
        Assert.Contains("Could not parse", exception.Message);
    }
}