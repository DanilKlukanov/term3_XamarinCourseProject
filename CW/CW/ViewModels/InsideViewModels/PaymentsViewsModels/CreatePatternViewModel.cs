using CW.Models;
using CW.Services;
using CW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels
{
    public class CreatePatternViewModel : BaseViewModel
    {
        public Pattern Pattern { get; private set; }
        public string Name { get; private set; }
        public string NumberFrom { get; private set; }
        public string NumberTo { get; private set; }
        public ValidationInput Amount { get; set; }
        public string TypeReceiver { get; set; }
        public ICommand ChangePatternCommand { get; private set; }
        public CreatePatternViewModel(Pattern pattern)
        {
            Pattern = pattern;
            Name = pattern.pattern_name;
            NumberFrom = pattern.from_;
            NumberTo = pattern.to_;
            Amount = new ValidationInput();
            Amount.Value = pattern.amount.ToString();
            ChangePatternCommand = new Command(ChangePattern);

            if (NumberTo.Length == 16)
            {
                TypeReceiver = "Карта получателя";
            } else
            {
                TypeReceiver = "Счет получателя";
            }
        }
        private async void ChangePattern()
        {
            if (Amount.Validate())
            {
                if (await Application.Current.MainPage.DisplayAlert("Подтверждение", "Вы уверены?", "Да", "Нет"))
                {
                    await PatternService.Instance.RemovePattern(Name);
                    string response = await PatternService.Instance.CreatePattern(Name, NumberFrom, NumberTo, int.Parse(Amount.Value));
                    await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                }
            }
        }
    }
}