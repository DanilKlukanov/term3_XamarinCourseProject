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
    public partial class NearbyBanksView : ContentPage
    {
        private NearbyBanksViewModel ViewModel { get; set; }
        public NearbyBanksView()
        {
            InitializeComponent();
        }

/*        public NearbyBanksView(LoginViewModel s) : this()
        {
            ViewModel = new NearbyBanksViewModel(s);
            BindingContext = ViewModel;
        }*/

        protected override void OnDisappearing() => (BindingContext as NearbyBanksViewModel).StartViewModel.ClosePageCommand.Execute(null);
    }
}