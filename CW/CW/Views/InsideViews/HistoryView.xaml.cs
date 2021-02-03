using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CW.ViewModels.InsideViewModels;

namespace CW.Views.InsideViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryView : ContentPage
    {
        public HistoryViewModel ViewModel { get; set; }
        private string _path = "CW.Views.InsideViews.";

        public HistoryView()
        {
            ViewModel = new HistoryViewModel(Navigation, _path + "HistoryView");
            SetData();
        }

        public HistoryView(HistoryViewModel viewModel)
        {
            ViewModel = viewModel;
            SetData();
        }

        private void SetData()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}