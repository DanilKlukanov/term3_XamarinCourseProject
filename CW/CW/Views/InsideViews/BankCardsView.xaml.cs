﻿using CW.ViewModels.InsideViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.InsideViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BankCardsView : ContentPage
    {
        public BankCardsView(BankItemViewModel bankItemViewModel)
        {
            InitializeComponent();
            BindingContext = bankItemViewModel;
        }
    }
}