using System.Diagnostics;
using PulseCheck.Core.Abstractions;

namespace PulseCheck.Core.Platform;

public class BashCommandRunner(string? interpreter = "/bin/bash") : ICommandRunner
{
    public string Run(string command)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = interpreter,
            Arguments = $"-c \"{command}\"",
            RedirectStandardOutput = true,
        };

        using var process = Process.Start(processStartInfo);
        if (process == null)
            throw new InvalidOperationException("Could not start process");

        process.WaitForExit();

        return process.StandardOutput.ReadToEnd().Trim();
    }
}