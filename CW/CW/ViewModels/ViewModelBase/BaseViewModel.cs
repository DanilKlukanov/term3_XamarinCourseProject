using System;
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
        
        public bool IsBusy { get; set; }

        protected async Task RunIsBusyTaskAsync(Func<Task> awaitableTask)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await awaitableTask();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}