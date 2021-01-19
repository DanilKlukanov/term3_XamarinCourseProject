using CW.ViewModels;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CW.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            BindingContext = new StartPageViewModel() { Navigation = this.Navigation };
        }
    }
}