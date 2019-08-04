using System;
using Xamarin.UITest;

namespace TestApp.UITest
{
    static class AppManager
    {
        const string ApkPath = "C:/Users/Sebbe/source/repos/todo-app/TestApp/TestApp.Android/bin/Release/Todo_App.Todo_App-Signed.apk"; // C:/Users/Sebbe/source/repos/todo-app/TestApp/TestApp.Android/bin/Release

        static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppManager.App' not set. Call 'AppManager.StartApp()' before trying to access it.");
                return app;
            }
        }

        static Platform? platform;
        public static Platform Platform
        {
            get
            {
                if (platform == null)
                    throw new NullReferenceException("'AppManager.Platform' not set.");
                return platform.Value;
            }

            set
            {
                platform = value;
            }
        }

        public static void StartApp()
        {
            if (Platform == Platform.Android)
            {
                app = ConfigureApp
                    .Android
                    .ApkFile(ApkPath)
                    .StartApp();
            }
        }
    }
}