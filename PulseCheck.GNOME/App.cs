using Gio;

using PulseCheck.Core.Abstractions;

namespace PulseCheck.GNOME;

public class App(ICommandRunner runner, ICommandParser parser)
{
    public void Run(string[] args)
    {
        var app = Adw.Application.New("io.github.mateusabelli", ApplicationFlags.FlagsNone);

        app.OnActivate += (sender, args) =>
        {
            var window = new MainWindow(app, runner, parser);
            window.Show();
        };

        app.Run(args);
    }
}