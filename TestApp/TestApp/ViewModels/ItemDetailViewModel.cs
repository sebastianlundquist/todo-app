using System;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Views;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public bool ShowReminder { get; set; }
        public Command EditItemCommand { get; set; }
        public Command DeleteItemCommand { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Title;
            Item = item;

            string reminder = item?.SetReminder;
            if (reminder == null)
            {
                ShowReminder = false;
            }
            else if (reminder == "true")
            {
                ShowReminder = true;
            }
            else
            {
                ShowReminder = false;
            }

            EditItemCommand = new Command(EditItem);
            DeleteItemCommand = new Command(DeleteItem);
        }

        async void EditItem()
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var page = new NewItemPage(Item, $"Edit {Item.Title}");
            page.Disappearing += (sender, e) =>
            {
                waitHandle.Set();
            };
            await App.Current.MainPage.Navigation.PushAsync(page, false);
            await Task.Run(() => waitHandle.WaitOne());
            MessagingCenter.Send(this, "EditItem", Item);
            await App.Current.MainPage.Navigation.PopAsync(false);
        }

        async void DeleteItem()
        {
            bool confirmed = await App.Current.MainPage.DisplayAlert("Confirm", $"Are you sure you want to delete item {Item.Title}?", "OK", "Cancel");
            if (confirmed)
            {
                App.Database.DeleteItem(Item);
                MessagingCenter.Send(this, "DeleteItem", Item);
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
