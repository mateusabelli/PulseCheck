using PulseCheck.Core.Platform;

namespace PulseCheck.Core.Tests;

public class BashCommandRunnerTests
{
    private readonly BashCommandRunner _runner = new("/bin/sh");

    [Fact]
    public void BashCommandRunner_RunsAndOutputs()
    {
        var stdout = _runner.Run("echo PulseCheck");
        Assert.Equal("PulseCheck", stdout);
    }
}