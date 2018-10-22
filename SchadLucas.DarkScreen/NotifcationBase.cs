using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SchadLucas.DarkScreen
{
    public abstract class NotifcationBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging([CallerMemberName] string propertyName = null) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }
}