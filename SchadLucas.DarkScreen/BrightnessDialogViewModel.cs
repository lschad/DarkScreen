namespace SchadLucas.DarkScreen
{
    internal sealed class BrightnessDialogViewModel : NotifcationBase
    {
        private int _brightness;

        public int Brightness
        {
            get => _brightness;
            set
            {
                _brightness = value;
                OnPropertyChanged();
            }
        }
    }
}