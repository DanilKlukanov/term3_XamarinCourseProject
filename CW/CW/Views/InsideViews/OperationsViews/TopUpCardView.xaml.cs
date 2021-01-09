using CW.Models;
using CW.ViewModels.InsideViewModels;
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
    public partial class TopUpCardView : ContentPage
    {
        public TopUpCardView(TopUpCardViewModel info)
        {
            InitializeComponent();
            BindingContext = info;
        }
    }
}