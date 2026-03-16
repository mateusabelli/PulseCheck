using Gtk;

using PulseCheck.Core.Abstractions;
using PulseCheck.Core.Domain;
using PulseCheck.Core.Services;

namespace PulseCheck.GNOME;

public class MainWindow : Adw.ApplicationWindow
{
    private readonly StabilityThresholds _thresholds;
    private readonly ResourceReader _resourceReader;

    private readonly ProgressBar _cpuBar;
    private readonly ProgressBar _ramBar;

    public MainWindow(Adw.Application app, ICommandRunner runner, ICommandParser parser)
    {
        _thresholds = new StabilityThresholds(85.0f, 95.0f);
        _resourceReader = new ResourceReader(runner, parser);

        var builder = Builder.NewFromFile("MainWindow.ui");

        _cpuBar = (ProgressBar)builder.GetObject("cpu_bar")!;
        _ramBar = (ProgressBar)builder.GetObject("ram_bar")!;
        var rootBox = (Box)builder.GetObject("root_box")!;

        Application = app;
        Title = "PulseCheck";
        SetDefaultSize(500, 500);

        SetContent(rootBox);

        GLib.Functions.TimeoutAdd(0, 1000, () =>
        {
            UpdateStats();
            return true;
        });
    }

    private void UpdateStats()
    {
        var snapshot = _resourceReader.ReadUsagePercent();
        var stability = SystemStabilityReader.GetCurrentState(snapshot, _thresholds);

        var cpuValue = snapshot.CpuUsagePercent;
        var ramValue = snapshot.MemoryUsagePercent;

        _cpuBar.SetFraction(cpuValue / 100);
        _cpuBar.SetText($"{cpuValue}%");

        _ramBar.SetFraction(ramValue / 100);
        _ramBar.SetText($"{ramValue}%");
    }
}