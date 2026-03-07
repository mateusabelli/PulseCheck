using Gio;

namespace PulseCheck.GNOME;

public class App
{
    public void Run(string[] args)
    {
        var app = Adw.Application.New("io.github.mateusabelli", ApplicationFlags.FlagsNone);

        app.OnActivate += (sender, args) =>
        {
            var window = new MainWindow(app);
            window.Show();
        };

        app.Run(args);
    }
}