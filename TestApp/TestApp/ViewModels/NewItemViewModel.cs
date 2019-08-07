using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Models;
using TestApp.Services;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public DateTime ReminderDate { get; set; }
        public TimeSpan ReminderTime { get; set; }
        private bool setReminder;

        public bool SetReminder
        {
            get { return setReminder; }
            set
            {
                setReminder = value;
                OnPropertyChanged();
            }
        }
        private readonly IDataStore<Item> Database;

        public Command SaveItemCommand { get; set; }
        public Command CancelCommand { get; set; }
        public NewItemViewModel(IDataStore<Item> database, Item item = null)
        {
            Database = database;

            Title = item?.Title;
            Item = item;
            ReminderDate = DateTime.Today;
            ReminderTime = DateTime.Now.TimeOfDay;

            string reminder = item?.SetReminder;
            if (reminder == null)
            {
                SetReminder = false;
            }
            else if (reminder == "true")
            {
                SetReminder = true;
            }
            else
            {
                SetReminder = false;
            }

            SaveItemCommand = new Command(SaveItem);
            CancelCommand = new Command(Cancel);
        }

        public async void SaveItem()
        {
            Item.ReminderTime = ReminderDate + ReminderTime;
            App.Database.SaveItem(Item);
            if (SetReminder)
            {
                Item.SetReminder = "true";
                CrossLocalNotifications.Current.Show(Item.Title, Item.Description, Item.Id, Item.ReminderTime);
            }
            else
            {
                Item.SetReminder = "false";
                CrossLocalNotifications.Current.Cancel(Item.Id);
            }
            App.Database.SaveItem(Item);
            MessagingCenter.Send(this, "EditItem", Item);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void Cancel()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
