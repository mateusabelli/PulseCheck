namespace PulseCheck.Core.Abstractions;

public interface ICommandRunner
{
    string Run(string command);
}