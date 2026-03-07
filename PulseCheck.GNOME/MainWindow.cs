using Gtk;

using PulseCheck.Core.Domain;
using PulseCheck.Core.Platform;
using PulseCheck.Core.Services;

namespace PulseCheck.GNOME;

public class MainWindow : Adw.ApplicationWindow
{
    private readonly ProgressBar _cpuBar;
    private readonly ProgressBar _ramBar;
    private readonly uint _timeoutId;

    public MainWindow(Adw.Application app)
    {
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

        _timeoutId = GLib.Functions.TimeoutAdd(0, 1000, () =>
        {
            UpdateStats();
            return true;
        });

        OnCloseRequest += (s, args) =>
        {
            GLib.Functions.SourceRemove(_timeoutId);
            return false;
        };

        Content = rootBox;
    }

    private void UpdateStats()
    {
        var runner = new BashCommandRunner();
        var parser = new BashOutputParser();
        var thresholds = new StabilityThresholds(85.0f, 95.0f);

        var resourceReader = new ResourceReader(runner, parser);

        var snapshot = resourceReader.ReadUsagePercent();
        var stability = SystemStabilityReader.GetCurrentState(snapshot, thresholds);

        var cpuValue = snapshot.CpuUsagePercent;
        var ramValue = snapshot.MemoryUsagePercent;

        _cpuBar.SetFraction(cpuValue / 100);
        _cpuBar.SetText($"{cpuValue}%");

        _ramBar.SetFraction(ramValue / 100);
        _ramBar.SetText($"{ramValue}%");
    }
}