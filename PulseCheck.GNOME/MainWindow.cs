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

        Application = app;
        Title = "PulseCheck";
        SetDefaultSize(500, 500);

        var rootBox = Box.New(Orientation.Vertical, 0);
        var headerBar = Adw.HeaderBar.New();
        rootBox.Append(headerBar);

        var verticalBox = Box.New(Orientation.Vertical, 16);
        verticalBox.SetMarginStart(24);
        verticalBox.SetMarginEnd(24);
        verticalBox.SetMarginTop(24);
        verticalBox.SetMarginBottom(24);

        verticalBox.Append(Label.New("CPU Usage"));
        _cpuBar = ProgressBar.New();
        _cpuBar.SetShowText(true);
        verticalBox.Append(_cpuBar);

        verticalBox.Append(Label.New("RAM Usage"));
        _ramBar = ProgressBar.New();
        _ramBar.SetShowText(true);
        verticalBox.Append(_ramBar);

        rootBox.Append(verticalBox);

        uint timeoutId = GLib.Functions.TimeoutAdd(0, 1000, () =>
        {
            UpdateStats();
            return true;
        });

        OnCloseRequest += (s, args) =>
        {
            GLib.Functions.SourceRemove(timeoutId);
            return false;
        };

        Content = rootBox;
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