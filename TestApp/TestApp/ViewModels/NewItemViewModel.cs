using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Models;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class NewItemViewModel : ItemDetailViewModel
    {
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

        public Command SaveItemCommand { get; set; }
        public Command CancelCommand { get; set; }
        public NewItemViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            ReminderDate = DateTime.Today;
            ReminderTime = DateTime.Now.TimeOfDay;
            SetReminder = false;
            SaveItemCommand = new Command(SaveItem);
            CancelCommand = new Command(Cancel);
        }

        public async void SaveItem()
        {
            Item.ReminderTime = ReminderDate + ReminderTime;
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
