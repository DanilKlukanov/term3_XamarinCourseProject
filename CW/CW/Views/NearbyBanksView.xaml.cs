using CW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyBanksView : ContentPage
    {
        private NearbyBanksViewModel ViewModel { get; set; }
        public NearbyBanksView()
        {
            InitializeComponent();
        }

        public NearbyBanksView(StartPageViewModel s) : this()
        {
            ViewModel = new NearbyBanksViewModel(s);
            BindingContext = ViewModel;
        }

        protected override void OnDisappearing() => ViewModel.StartViewModel.ClosePageCommand.Execute(null);
    }
}