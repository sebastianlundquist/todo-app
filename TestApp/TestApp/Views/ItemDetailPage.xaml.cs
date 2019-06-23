using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestApp.Models;
using TestApp.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void EditItem_Clicked(object sender, EventArgs e)
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var page = new NavigationPage(new NewItemPage(viewModel.Item));
            page.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await Navigation.PushModalAsync(page, false);
            await Task.Run(() => waitHandle.WaitOne());
            MessagingCenter.Send(this, "EditItem", viewModel.Item);
            await Navigation.PopAsync(false);
        }

        async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            bool confirmed = await DisplayAlert("Confirm", $"Are you sure you want to delete item {viewModel.Item.Text}?", "OK", "Cancel");
            if (confirmed)
            {
                App.Database.DeleteItem(viewModel.Item);
                MessagingCenter.Send(this, "EditItem", viewModel.Item);
                await Navigation.PopAsync();
            }
        }
    }
}