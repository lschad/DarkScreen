using System;
using System.Windows.Input;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SchadLucas.DarkScreen
{
    public partial class BrightnessDialog
    {
        private static App App => (App)Application.Current;
        private BrightnessDialogViewModel ViewModel => (BrightnessDialogViewModel)DataContext;
        public static bool IsOpen { get; private set; }

        private BrightnessDialog()
        {
            InitializeComponent();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            const int highValue = 10;
            var lowValue = 1;

            if (Keyboard.IsKeyDown(Key.LeftAlt))
            {
                lowValue = 5;
            }

            switch (e.Key)
            {
                case Key.PageUp:
                    App.BrightnessUp(highValue);
                    break;

                case Key.PageDown:
                    App.BrightnessDown(highValue);
                    break;

                case Key.Left:
                    App.BrightnessDown(lowValue);
                    break;

                case Key.Right:
                    App.BrightnessUp(lowValue);
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void OnMouseUpClose(object sender, MouseButtonEventArgs e)
        {
            Close();
            IsOpen = false;
        }

        public static void Show(Action<float> valueChangedCallback)
        {
            if (!IsOpen)
            {
                IsOpen = true;

                var window = new BrightnessDialog();
                var brightness = (int)(App.Opacity * 100f);
                var vm = new BrightnessDialogViewModel { Brightness = brightness };

                vm.PropertyChanged += (s, e) => valueChangedCallback(vm.Brightness);

                window.DataContext = vm;
                window.ShowDialog();
            }
        }
    }
}