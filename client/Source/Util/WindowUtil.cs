using Avalonia.Controls;
using Avalonia.Input;

namespace SealSoundMaker.Source.Util
{
    public static class WindowUtil
    {
        public static void TopBarPointerPressed(Window wnd, PointerPressedEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                wnd.WindowState = wnd.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
                return;
            }

            if (e.GetCurrentPoint(wnd).Properties.IsLeftButtonPressed)
                wnd.BeginMoveDrag(e);
        }
    }
}
