using Moq;

using PulseCheck.Core.Abstractions;
using PulseCheck.Core.Services;

namespace PulseCheck.Core.Tests.Services;

public class ResourceReaderTests
{
    private const string CmdMemory = "free -m | grep Mem | awk '{print $2, $3}'";
    private const string CmdCpu = "top -b -n 1 | grep ^%Cpu | awk '{print $8}'";

    private readonly Mock<ICommandRunner> _runner = new();
    private readonly Mock<ICommandParser> _parser = new();

    [Fact]
    public void ReadUsagePercent_ShouldReturnSnapshot()
    {
        _parser.Setup(p => p.ParseCpuUsagePercent(It.IsAny<string>())).Returns(33f);
        _parser.Setup(p => p.ParseMemoryUsagePercent(It.IsAny<string>())).Returns(25.8f);

        var resourceReader = new ResourceReader(_runner.Object, _parser.Object);
        var resourceSnapshot = resourceReader.ReadUsagePercent();

        Assert.Equal(33f, resourceSnapshot.CpuUsagePercent, 1);
        Assert.Equal(25.8, resourceSnapshot.MemoryUsagePercent, 1);
    }

    [Theory]
    [InlineData("31866 6498", 20.39f)]
    [InlineData("4096 2048", 50f)]
    [InlineData("8192 0", 0f)]
    public void MemoryUsagePercent_ShouldCalculateCorrectly(string stdout, float expected)
    {
        _runner.Setup(r => r.Run(CmdMemory)).Returns(stdout);
        _parser.Setup(p => p.ParseMemoryUsagePercent(stdout)).Returns(expected);

        var resourceReader = new ResourceReader(_runner.Object, _parser.Object);
        var memoryUsagePercent = resourceReader.ReadUsagePercent().MemoryUsagePercent;

        Assert.Equal(expected, memoryUsagePercent, 1);
    }

    [Fact]
    public void MemoryUsagePercent_WhenParserThrows_ShouldPropagate()
    {
        _runner.Setup(r => r.Run(CmdMemory)).Returns(It.IsAny<string>());
        _parser.Setup(p => p.ParseMemoryUsagePercent(It.IsAny<string>()))
            .Throws(new InvalidOperationException("Could not parse"));

        var resourceReader = new ResourceReader(_runner.Object, _parser.Object);
        var exception = Assert.Throws<InvalidOperationException>(() => resourceReader.ReadUsagePercent());
        Assert.Contains("Could not parse", exception.Message);
    }

    [Theory]
    [InlineData("88.5", 11.5f)]
    [InlineData("22.8", 77.2f)]
    [InlineData("0", 100f)]
    public void CpuUsagePercent_ShouldCalculateCorrectly(string stdout, float expected)
    {
        _runner.Setup(r => r.Run(CmdCpu)).Returns(stdout);
        _parser.Setup(p => p.ParseCpuUsagePercent(stdout)).Returns(expected);

        var resourceReader = new ResourceReader(_runner.Object, _parser.Object);
        var cpuUsagePercent = resourceReader.ReadUsagePercent().CpuUsagePercent;

        Assert.Equal(expected, cpuUsagePercent, 1);
    }

    [Fact]
    public void CpuUsagePercent_WhenParserThrows_ShouldPropagate()
    {
        _runner.Setup(r => r.Run(CmdCpu)).Returns(It.IsAny<string>());
        _parser.Setup(p => p.ParseCpuUsagePercent(It.IsAny<string>()))
            .Throws(new InvalidOperationException("Could not parse"));

        var resourceReader = new ResourceReader(_runner.Object, _parser.Object);
        var exception = Assert.Throws<InvalidOperationException>(() => resourceReader.ReadUsagePercent());
        Assert.Contains("Could not parse", exception.Message);
    }

    [Fact]
    public void WhenRunnerThrows_ShouldPropagate()
    {
        _runner.Setup(r => r.Run(It.IsAny<string>()))
            .Throws(new InvalidOperationException("Could not start process"));

        var resourceReader = new ResourceReader(_runner.Object, _parser.Object);
        var exception = Assert.Throws<InvalidOperationException>(() => resourceReader.ReadUsagePercent());
        Assert.Contains("Could not start process", exception.Message);
    }
}