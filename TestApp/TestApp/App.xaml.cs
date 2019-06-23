﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestApp.Services;
using TestApp.Views;
using System.IO;
using TestApp.Models;

namespace TestApp
{
    public partial class App : Application
    {
        public static IDataStore<Item> Database => DependencyService.Get<IDataStore<Item>>() ?? new SqliteDataStore(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Items.db3"));

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
