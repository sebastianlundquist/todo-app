using System;
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
