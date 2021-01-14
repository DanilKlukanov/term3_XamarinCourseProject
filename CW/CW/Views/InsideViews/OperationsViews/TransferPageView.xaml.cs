using CW.ViewModels.InsideViewModels.OperationsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.InsideViews.OperationsViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferPageView : ContentPage
    {
        public TransferPageView(TransferPageViewModel info)
        {
            InitializeComponent();
            BindingContext = info;
        }
    }
}