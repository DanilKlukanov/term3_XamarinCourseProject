using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels
{
    public class DialogsViewModel : BaseInsideViewModel
    {

        public DialogsViewModel(INavigation navigation) : base(navigation)
        {
            OpenProfilePageCommand = new Command(OpenProfilePage);
        }
        
        public ICommand OpenProfilePageCommand { get; private set; }
        
        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }
    }
}
