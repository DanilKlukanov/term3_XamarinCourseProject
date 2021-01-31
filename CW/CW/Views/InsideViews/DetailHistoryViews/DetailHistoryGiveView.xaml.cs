using CW.ViewModels.InsideViewModels.DetailHistoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.InsideViews.DetailHistoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailHistoryGiveView : ContentPage
    {
        public DetailHistoryGiveView(DetailHistoryGiveModel info)
        {
            InitializeComponent();
            BindingContext = info;
        }
    }
}