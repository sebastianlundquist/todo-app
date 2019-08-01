﻿using Plugin.LocalNotifications;
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
            Item.SetReminder = SetReminder ? "true" : "false";

            CrossLocalNotifications.Current.Show(Item.Text, Item.Description, 101, Item.ReminderTime);

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
