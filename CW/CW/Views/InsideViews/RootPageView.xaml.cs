﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.InsideViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPageView : Shell
    {
        public RootPageView()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {

            bool isRoot = true;

            foreach (var item in tabs.Items)
            {
                if (item?.Stack?.Count > 1)
                {
                    isRoot = false;
                    break;
                }
            }

            base.OnBackButtonPressed();


            if (isRoot)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await DisplayAlert("Выход", "Вы уверены, что хотите выйти?", "Да", "Нет");

                    if (result)
                    {
                        MessagingCenter.Send(this, "exit");
                    }
                });
            }

            return true;
        }

        protected override void OnNavigating(ShellNavigatingEventArgs e)
        {
            base.OnNavigating(e);

            if (e.Source != ShellNavigationSource.ShellSectionChanged)
            {
                return;
            }

            foreach (var item in tabs.Items)
            {
                if (item?.Stack?.Count > 1)
                {
                    item.Stack[1].Navigation.PopToRootAsync();
                }
            }
        }
    }
}