using CW.Models;
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
        public ICommand ChangePatternCommand { get; private set; }
        public CreatePatternViewModel(Pattern pattern)
        {
            Pattern = pattern;
            ChangePatternCommand = new Command(ChangePattern);
        }
        private async void ChangePattern()
        {

        }
    }
}