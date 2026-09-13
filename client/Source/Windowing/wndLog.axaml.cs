using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Input;
using Avalonia.Media;
using SealSoundMaker.Source.Util;
using SealSoundMaker.Util;
using System.Collections.Generic;

namespace SealSoundMaker.Source.Windowing
{
    public partial class wndLog : Window
    {
        private Dictionary<LogType, SolidColorBrush> logColors = new()
        {
            {LogType.INFO, new SolidColorBrush(Colors.LightBlue) },
            {LogType.WARN, new SolidColorBrush(Colors.Orange) },
            {LogType.ERROR, new SolidColorBrush(Colors.Red) },
        };


        public wndLog()
        {
            InitializeComponent();
            Log.onNewMessage = AddMessage;

            foreach (LogMessage mes in Log.GetMessages())
            {
                AddMessage(mes);
            }
        }

        public void AddMessage(LogMessage mes)
        {
            stbLog.Inlines!.Add(new Run(mes.Format() + "\n") { Foreground = logColors[mes.type] });
        }

        private void Grid_PointerPressed(object? sender, PointerPressedEventArgs e) => WindowUtil.TopBarPointerPressed(this, e);
    }
}