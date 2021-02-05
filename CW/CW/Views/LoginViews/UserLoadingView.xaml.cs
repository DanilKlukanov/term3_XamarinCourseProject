using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.LoginViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLoadingView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public UserLoadingView()
        {
            InitializeComponent();
        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}