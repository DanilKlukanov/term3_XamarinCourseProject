using CW.Models;
using CW.Services;
<<<<<<< HEAD
=======
using CW.Validations;
>>>>>>> Add Validations for fields on dialogs screen
using CW.ViewModels.InsideViewModels;
using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CW.ViewModels
{
    public class DialogsViewModel : BaseViewModel
    {
        private bool _isButtonEnabled;
        public ObservableCollection<Message> Messages { get; set; }
<<<<<<< HEAD
=======


>>>>>>> Add Validations for fields on dialogs screen
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }
        public INavigation Navigation { get; private set; }

        public INavigation Navigation { get; set; }

        public ValidatableObject<string> Recipient { get; set; }
        public ValidatableObject<string> Message { get; set; }

        public DialogsViewModel(INavigation navigation)
        {
<<<<<<< HEAD
            Navigation = navigation;
            _isButtonEnabled = true;
=======
            AddValidations();

>>>>>>> Add Validations for fields on dialogs screen
            Messages = new ObservableCollection<Message>();

            Navigation = navigation;

            SendMessageCommand = new Command(SendMessage);
<<<<<<< HEAD
            OpenProfilePageCommand = new Command(OpenProfilePage, () => IsButtonEnabled);
=======
            OpenProfilePageCommand = new Command(OpenProfile);

>>>>>>> Add Validations for fields on dialogs screen
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

<<<<<<< HEAD
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
=======
        private void OpenProfile(object obj)
>>>>>>> Add Validations for fields on dialogs screen
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }

        private async void SendMessage()
        {
            if (Validate())
            {
                var response = await new DialogService().SendMessage(Recipient.Value, Message.Value);

                if (response.IsSuccessful)
                    await Application.Current.MainPage.DisplayAlert("Уведомление", response.Value, "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Уведомление", response.ErrorMessage, "OK");
            }
        }

        private async void GetMessages()
        {
            var messages = await new DialogService().GetMessages();
            var tmpMessages = new ObservableCollection<Message>();

            foreach (var item in messages)
            {
                Color col = Color.LightGreen;
                if (item.from_ == App.GetUser().login)
                {
                    col = Color.LightBlue;
                }
                tmpMessages.Add(new Message()
                {
                    from_ = item.from_,
                    to_ = item.to_,
                    msg = item.msg,
                    msg_time = DateTime.Parse(item.msg_time).ToString("MM/dd/yyyy HH:mm:ss"),
                    col = col
                });
            }



            Messages = tmpMessages;
            OnPropertyChanged("Messages");
        }

        private bool Validate()
        {
            return Recipient.Validate() && Message.Validate();
        }


        private void AddValidations()
        {
            Recipient = new ValidatableObject<string>();
            Message = new ValidatableObject<string>();

            Recipient.Validations.Add(new IsNotNullOrEmptyRule<string> {
                ValidationMessage = "Введите имя получателя."
            });
            Message.Validations.Add(new IsNotNullOrEmptyRule<string> {
                ValidationMessage = "Введите текст сообщения."
            });

        }


        private void Refresh()
        {
            //Device.Sto
            GetMessages();
        }
    }
}
