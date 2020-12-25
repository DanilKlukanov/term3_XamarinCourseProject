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
        public MainScreenView()
        {
            InitializeComponent();
            BindingContext = new MainScreenViewModel(Navigation);
        }
    }
}