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
<<<<<<< HEAD:CW/CW/Views/InsideViews/ApplicationInfoView.xaml.cs
    public partial class ApplicationInfoView : ContentPage
    {
        public ApplicationInfoView()
=======
    public partial class DetailHistoryGetView : ContentPage
    {
        public DetailHistoryGetView(DetailHistoryGetModel info)
>>>>>>> Fix pattern:CW/CW/Views/InsideViews/DetailHistoryViews/DetailHistoryGetView.xaml.cs
        {
            InitializeComponent();
            BindingContext = info;
        }
    }
}