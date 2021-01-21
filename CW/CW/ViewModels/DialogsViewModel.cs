using CW.Models;
using CW.Services;
using CW.ViewModels.InsideViewModels;
using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CW.ViewModels
{
    public class DialogsViewModel : BaseViewModel
    {
        private bool _isButtonEnabled;
        public ObservableCollection<Message> Messages { get; set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }
        public INavigation Navigation { get; private set; }

        public string Recipient { get; set; }
        public string Message { get; set; }

        public DialogsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _isButtonEnabled = true;
            Messages = new ObservableCollection<Message>();
            SendMessageCommand = new Command(SendMessage);
            OpenProfilePageCommand = new Command(OpenProfilePage, () => IsButtonEnabled);
            GetMessages();
            Device.StartTimer(TimeSpan.FromSeconds(15), () => { 
                GetMessages();
                return true; 
            });
        }
        private bool IsButtonEnabled
        {
            get => _isButtonEnabled;

            set
            {
                if (value != _isButtonEnabled)
                {
                    _isButtonEnabled = value;

                    (OpenProfilePageCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        private async void OpenProfilePage()
        {
            IsButtonEnabled = false;
            await Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
            IsButtonEnabled = true;
        }
        private async void SendMessage()
        {
            var response = await new DialogService().SendMessage(Recipient, Message);

            if (response.IsSuccessful)
                await Application.Current.MainPage.DisplayAlert("Уведомление", response.Value, "OK");
            else
                await Application.Current.MainPage.DisplayAlert("Уведомление", response.ErrorMessage, "OK");

        }

        private async void GetMessages()
        {
            var messages = await new DialogService().GetMessages();
            Messages.Clear();

            foreach (var item in messages)
            {
                Color col = Color.LightGreen;
                if (item.from_ == App.GetUser().login)
                {
                    col = Color.LightBlue;
                }
                Messages.Add(new Message()
                {
                    from_ = item.from_,
                    to_ = item.to_,
                    msg = item.msg,
                    msg_time = DateTime.Parse(item.msg_time).ToString("MM/dd/yyyy HH:mm:ss"),
                    col = col
                });
            }

            //response.ForEach(x => Messages.Add(x));
        }

        private void Refresh()
        {
            //Device.Sto
            GetMessages();
        }
    }
}
