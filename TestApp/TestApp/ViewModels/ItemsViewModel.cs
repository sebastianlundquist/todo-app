using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TestApp.Models;
using TestApp.Views;
using System.Linq;
using TestApp.Services;

namespace TestApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private bool noItemsExist;
        public bool NoItemsExist
        {
            get
            {
                return noItemsExist;
            }
            set
            {
                noItemsExist = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Item> Items { get; set; }
        
        public Command AddItemCommand { get; set; }
        public Command LoadItemsCommand { get; set; }

        private readonly IDataStore<Item> db;
        public ItemsViewModel(IDataStore<Item> database)
        {
            db = database ?? throw new ArgumentNullException(nameof(database));
            Title = "TODO List";
            Items = new ObservableCollection<Item>();
            AddItemCommand = new Command(async () => {
                await App.Current.MainPage.Navigation.PushAsync(new NewItemPage(new Item(), "New Item"));
            });
            LoadItemsCommand = new Command(LoadItems);

            MessagingCenter.Subscribe<NewItemViewModel, Item>(this, "EditItem", (obj, item) =>
            {
                LoadItems();
            });

            MessagingCenter.Subscribe<ItemDetailViewModel, Item>(this, "DeleteItem", (obj, item) =>
            {
                LoadItems();
            });
        }

        public void LoadItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = db.GetItems();
                foreach (var item in items)
                    Items.Add(item);
                if (items.Count() == 0)
                    NoItemsExist = true;
                else
                    NoItemsExist = false;
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