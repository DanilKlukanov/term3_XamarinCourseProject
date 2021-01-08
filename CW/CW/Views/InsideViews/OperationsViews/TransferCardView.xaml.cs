using CW.Models;
using CW.ViewModels.InsideViewModels.OperationsViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.InsideViews.Operations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferCardView : ContentPage
    {
        public TransferCardView(TransferCardViewModel info)
        {
            InitializeComponent();
            BindingContext = info;
        }
    }
}