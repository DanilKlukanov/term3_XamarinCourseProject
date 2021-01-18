using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CW.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public virtual async Task InitializeAsync(object parameter)
        {
            return;
        }

    }
}