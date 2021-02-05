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
    public partial class PaymentsView : ContentPage
    {
        PaymentsViewModel ViewModel;
        public PaymentsView()
        {
            InitializeComponent();
            ViewModel = new PaymentsViewModel(Navigation);
            BindingContext = ViewModel;
            Appearing += UpdatePageInfo;
        }
        private async void UpdatePageInfo(object sender, EventArgs e)
        {
            await ViewModel.LoadAllItems();
        }
    }
}