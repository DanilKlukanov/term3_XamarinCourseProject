using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CW.ViewModels;
using System.Collections.ObjectModel;

namespace CW.Views.InsideViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogsView : ContentPage
    {
        public DialogsView()
        {
            InitializeComponent();
            BindingContext = new DialogsViewModel(Navigation);
        }
    }
}