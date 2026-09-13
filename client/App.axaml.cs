using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SealSoundMaker.Audio;
using SealSoundMaker.Util;
using SealSoundMaker.Windowing;

namespace SealSoundMaker;

public partial class App : Application
{
    public const string VERSION = "1.0.0-dev";
    public const string VER_DATE = "13.09.2026";

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        Log.Init();
        Log.Info($"SealSoundMaker {VERSION} ({VER_DATE})");

        AudioHandler.Init();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}