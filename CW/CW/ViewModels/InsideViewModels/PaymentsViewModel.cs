using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Models;
using System.Collections.ObjectModel;

namespace CW.ViewModels.InsideViewModels
{
    public class PaymentsViewModel : BaseViewModel
    {
        private bool _isEnabled;

        public PaymentsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _isEnabled = true;

            ListTamplates();

            CreateTemplatesCommand = new Command(OpenCreateTemplates);
            OpenProfilePageCommand = new Command(OpenProfilePage);
            BackCommand = new Command(Back, () => _isEnabled);
        }

        public ObservableCollection<NameOperationInTemplate> PayTemplates { get; private set; }

        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand CreateTemplatesCommand { get; private set; }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void ListTamplates()
        {
            PayTemplates = new ObservableCollection<NameOperationInTemplate>
            {
                new NameOperationInTemplate
                {
                    Name = "Перевод между своими счетами и картами",
                    NameOperation = "Перевод",
                    Money = 1
                },
                new NameOperationInTemplate
                {
                    Name = "Данил Вадимович У.",
                    NameOperation = "Перевод",
                    Money = 1800
                },
                new NameOperationInTemplate
                {
                    Name = "МТС",
                    NameOperation = "Оплата услуг",
                    Money = 550
                },
                new NameOperationInTemplate
                {
                    Name = "Перевож между своими счетами и картами",
                    NameOperation = "Пополнение баланса",
                    Money = 150
                },
            };
        }

        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }
        private void OpenCreateTemplates()
        {
            Navigation.PushAsync(new CreateTemplatesView(new CreateTemplatesViewModel(Navigation)));
        }
    }
}
