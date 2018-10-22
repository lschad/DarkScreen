using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using WpfScreenHelper;

namespace SchadLucas.DarkScreen
{
    internal class OverlayWindow : Window
    {
        public OverlayWindow(Screen screen, float opacity, Color color = default(Color))
        {
            Opacity = opacity;
            Background = new SolidColorBrush(color);
            Left = screen.Bounds.Left;
            Top = screen.Bounds.Top;
            Width = screen.Bounds.Width;
            Height = screen.Bounds.Height;

            ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Topmost = true;
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.Manual;

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var wndHelper = new WindowInteropHelper(this);
            var exStyle = WindowsServices.GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);
            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            WindowsServices.SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, exStyle);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }
    }
}