using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using SealSoundMaker.Audio;
using SealSoundMaker.Source.Util;
using SealSoundMaker.Source.Windowing;
using SoundFlow.Abstracts.Devices;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Structs;
using System.IO;
using System.Linq;

namespace SealSoundMaker.Windowing;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        tblVersion.Text = $"Version {App.VERSION}";
    }

    private void Grid_PointerPressed(object? sender, PointerPressedEventArgs e) => WindowUtil.TopBarPointerPressed(this, e);

    private void miLog_Click(object? sender, RoutedEventArgs e)
    {
        wndLog log = new wndLog();
        log.Show();
    }

    private void btnRecord_Click(object? sender, RoutedEventArgs e)
    {
        Recording.Start("albon.wav");
    }

    private void btnStopRecording_Click(object? sender, RoutedEventArgs e)
    {
        Recording.Stop();
    }

    private void btnPlayback_Click(object? sender, RoutedEventArgs e)
    {
        Playback.Play("albon.wav");
    }
}