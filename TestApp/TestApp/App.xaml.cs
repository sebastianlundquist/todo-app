using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestApp.Services;
using TestApp.Views;
using System.IO;
using TestApp.Models;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace TestApp
{
    public partial class App : Application
    {
        static IDataStore<Item> database;
        public static IDataStore<Item> Database
        {
            get
            {
                if (database == null)
                {
                    database = new SqliteDataStore(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Issues.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<SqliteDataStore>();
            MainPage = new NavigationPage(new ItemsPage());
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=7b403b13-6690-4b6d-ba7e-1a45575b04de;",
                  typeof(Analytics), typeof(Crashes));
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
