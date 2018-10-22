using SchadLucas.ToastNotifications;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Screen = WpfScreenHelper.Screen;

namespace SchadLucas.DarkScreen
{
    public partial class App
    {
        private readonly Color _overlayColor = Color.FromArgb(255, 25, 25, 25);
        private readonly List<Window> _windows = new List<Window>();
        private readonly List<GlobalKeyboardHook.VKeys> _pressedKeys = new List<GlobalKeyboardHook.VKeys>();
        private Tray _tray;

        internal float Opacity
        {
            get => (float)(_windows.FirstOrDefault()?.Opacity ?? 0f);
            private set
            {
                foreach (var window in _windows)
                {
                    window.Opacity = value;
                }
            }
        }

        private static float NormalizeBrightness(float brightness) => brightness / 100f;

        private void OnBrightnessChanged(float brightness)
        {
            Opacity = NormalizeBrightness(brightness);
        }

        private void SetupScreens()
        {
            foreach (var screen in Screen.AllScreens)
            {
                var window = new OverlayWindow(screen, 0.25f, _overlayColor);
                _windows.Add(window);
                window.Show();
            }
        }

        private void SetupTray()
        {
            _tray = new Tray(DarkScreen.Properties.Resources.TrayIcon);
            _tray.CreateMenuItem("Brightness", () => BrightnessDialog.Show(OnBrightnessChanged), () => !BrightnessDialog.IsOpen);
            _tray.CreateMenuItem("Exit", () => Shutdown(0));
        }

        private void SetupKeyboardHook()
        {
            void OnKeyDown(GlobalKeyboardHook.VKeys key)
            {
                if (!_pressedKeys.Contains(key))
                {
                    _pressedKeys.Add(key);
                }

                if (_pressedKeys.Count == 3 && _pressedKeys.Contains(GlobalKeyboardHook.VKeys.LCONTROL) && _pressedKeys.Contains(GlobalKeyboardHook.VKeys.LMENU))
                {
                    if (_pressedKeys.Contains(GlobalKeyboardHook.VKeys.PRIOR)) BrightnessUp(10);
                    if (_pressedKeys.Contains(GlobalKeyboardHook.VKeys.NEXT)) BrightnessDown(10);
                }
            }

            void OnKeyUp(GlobalKeyboardHook.VKeys key)
            {
                _pressedKeys.Remove(key);
            }

            var hook = new GlobalKeyboardHook();
            hook.KeyUp += OnKeyUp;
            hook.KeyDown += OnKeyDown;
            hook.Install();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _tray.Close();
            ToastNotificationManager.Uninitialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            SetupTray();
            SetupScreens();
            SetupKeyboardHook();

            ToastNotificationManager.Initialize("SchadLucas.DarkScreen");
            ToastNotificationManager.NewNotification();
        }

        public void BrightnessUp(int i)
        {
            var j = NormalizeBrightness(i);

            var value = Opacity + j;
            if (value > .95)
            {
                value = .95f;
            }

            Opacity = value;
        }

        public void BrightnessDown(int i)
        {
            var j = NormalizeBrightness(i);

            var value = Opacity - j;
            if (value < 0)
            {
                value = 0;
            }

            Opacity = value;
        }
    }
}