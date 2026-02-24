using PulseCheck.Core.Platform;

namespace PulseCheck.Core.Tests.Platform;

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