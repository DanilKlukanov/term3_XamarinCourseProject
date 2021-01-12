using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CW.ViewModels.InsideViewModels.PaymentsViewsModels;

namespace CW.Views.InsideViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferToClientView : ContentPage
    {
        public TransferToClientView(TransferToClientViewModel info)
        {
            InitializeComponent();
            BindingContext = info;
        }
    }
}