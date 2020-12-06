using CW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : ContentPage
    {
        private MapPageViewModel ViewModel { get; set; }
        public Map()
        {
            InitializeComponent();           
        }

        public Map(StartPageViewModel s) : this()
        {
            ViewModel = new MapPageViewModel(s);
            BindingContext = ViewModel;
        }

        protected override void OnDisappearing() => ViewModel.StartViewModel.ClosePageCommand.Execute(null);
    }
}