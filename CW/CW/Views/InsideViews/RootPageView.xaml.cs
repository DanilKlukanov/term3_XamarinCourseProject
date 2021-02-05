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
    public partial class RootPageView : Shell
    {
        public RootPageView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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

           

             UpdatePages();
            //UpdateChildrenLayout();

        }

        private void UpdatePages()
        {
            /*foreach (var tab in tabs.Items)
            {
                if (tab.CurrentItem.Content != null)
                {
                    tab.CurrentItem.Content.LoadFromXaml(tab.Title);
                }

                tab.CurrentItem.ContentTemplate.CreateContent();
            }*/

            /*List<DataTemplate> templates = new List<DataTemplate>()
            {
                new DataTemplate(() => new MainScreenView()),
                new DataTemplate(() => new PaymentsView()),
                new DataTemplate(() => new HistoryView()),
                new DataTemplate(() => new DialogsView())
            };
            foreach (var tab in tabs.Items)
            {
                tab.CurrentItem.ContentTemplate.CreateContent();
            }*/

            //tabs.CurrentItem.CurrentItem.ContentTemplate.CreateContent();

            //int index = tabs.CurrentItem.TabIndex;

            //tabs.Items[index].Items[0].ContentTemplate = templates[index];
            //tabs.Items[index].CurrentItem.ContentTemplate.CreateContent();
            //tabs.Items.RemoveAt(index);
            //tabs.Items.Insert(index, shellContents[index]);

            /*tabs.Items.Clear();

            var shellContents = new List<ShellContent>()
            {
                new ShellContent()
                {
                    Content = new MainScreenView(),
                    Title = "Главный",
                    Icon="main.png"
                },
                new ShellContent()
                {
                    Content = new PaymentsView(),
                    Title = "Платежи",
                    Icon="credit_card.png"
                },
                new ShellContent()
                {
                    Content = new HistoryView(),
                    Title = "История",
                    Icon="history.png"
                },
                new ShellContent()
                {
                    Content = new DialogsView(),
                    Title = "Диалоги",
                    Icon="speech_bubble.png"
                }
            };

            shellContents.ForEach(x => tabs.Items.Add(x));

           *//* var shellContent = new ShellContent()
            {
                Content = new MainScreenView(),
                Title = "Main"
            };*/

            /*            tabs.Items.Add(shellContent);

                        shellContent = new ShellContent()
                        {
                            Content = new PaymentsView(),
                            Title = "Payments"
                        };

                        tabs.Items.Add(shellContent);

                        shellContent = new ShellContent()
                        {
                            Content = new HistoryView(),
                            Title = "History"
                        };

                        tabs.Items.Add(shellContent);

                        shellContent = new ShellContent()
                        {
                            Content = new DialogsView(),
                            Title = "Dialogs"
                        };

                        tabs.Items.Add(shellContent);*/
        }
    }
}