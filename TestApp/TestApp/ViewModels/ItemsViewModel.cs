using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TestApp.Models;
using TestApp.Views;
using System.Linq;

namespace TestApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "TODO List";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(LoadItems);

            MessagingCenter.Subscribe<NewItemViewModel, Item>(this, "EditItem", (obj, item) =>
            {
                LoadItems();
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", (obj, item) =>
            {
                LoadItems();
            });
        }

        void LoadItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = App.Database.GetItems();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}