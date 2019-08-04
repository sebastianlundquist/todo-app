using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace TestApp.UITest
{
    public class NewItemPage : BasePage
    {
        readonly Query cancelButton;
        readonly Query saveButton;
        readonly Query titleEntry;
        readonly Query descriptionEditor;
        readonly Query setReminderSwitch;
        readonly Query reminderDatePicker;
        readonly Query reminderTimePicker;
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("titleEntry")
        };

        public NewItemPage()
        {
            cancelButton = x => x.Marked("cancelButton");
            saveButton = x => x.Marked("saveButton");
            titleEntry = x => x.Marked("titleEntry");
            descriptionEditor = x => x.Marked("descriptionEditor");
            setReminderSwitch = x => x.Marked("setReminderSwitch");
            reminderDatePicker = x => x.Marked("reminderDatePicker");
            reminderTimePicker = x => x.Marked("reminderTimePicker");
        }

        public NewItemPage TapCancel()
        {
            app.WaitForElement(cancelButton);
            app.Tap(cancelButton);
            app.Screenshot("Tapped cancel button");
            return this;
        }

        public NewItemPage TapSave()
        {
            app.WaitForElement(saveButton);
            app.Tap(saveButton);
            app.Screenshot("Tapped save button");
            return this;
        }

        public NewItemPage EnterTitle(string title)
        {
            app.WaitForElement(titleEntry);
            app.ClearText(titleEntry);
            app.EnterText(titleEntry, title);
            app.DismissKeyboard();
            app.Screenshot("Entered title");
            return this;
        }

        public NewItemPage EnterDescription(string description)
        {
            app.WaitForElement(descriptionEditor);
            app.ClearText(descriptionEditor);
            app.EnterText(descriptionEditor, description);
            app.DismissKeyboard();
            app.Screenshot("Entered description");
            return this;
        }

        public NewItemPage ToggleReminderOn()
        {
            var isOn = app.Query(x => x.Marked("setReminderSwitch").Invoke("isChecked").Value<bool>())[0];
            var result = app.WaitForElement(setReminderSwitch)[0];
            if (!isOn)
            {
                app.TapCoordinates(result.Rect.CenterX, result.Rect.CenterY);
                app.Screenshot("Toggled reminder on");
            }
            return this;
        }

        public NewItemPage ToggleReminderOff()
        {
            var isOn = app.Query(x => x.Marked("setReminderSwitch").Invoke("isChecked").Value<bool>())[0];
            var result = app.WaitForElement(setReminderSwitch)[0];
            if (isOn)
            {
                app.TapCoordinates(result.Rect.CenterX, result.Rect.CenterY);
                app.Screenshot("Toggled reminder off");
            }
            return this;
        }
    }
}