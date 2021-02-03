using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CW.ViewModels.InsideViewModels;

namespace CW.Views.InsideViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainScreenView : ContentPage
    {

        MainScreenViewModel ViewModel;
        public MainScreenView()
        {
            InitializeComponent();
            ViewModel = new MainScreenViewModel(Navigation);
            BindingContext = ViewModel;
            Appearing += UpdatePageInfo;
        }

        private async void UpdatePageInfo(object sender, EventArgs e)
        {
           ViewModel.RefreshCommand.Execute(null);
        }
    }
}