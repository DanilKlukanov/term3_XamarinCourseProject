using CW.ViewModels.InsideViewModels.PaymentsViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.InsideViews.PaymentsViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferBetweenView : ContentPage
    {
        public TransferBetweenView(TransferBetweenViewModel info)
        {
            InitializeComponent();
            BindingContext = info;
        }
    }
}