using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace TestApp.UITest
{
    public class ItemDetailPage : BasePage
    {
        readonly Query editButton;
        readonly Query deleteButton;
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("detailsContainer")
        };

        public ItemDetailPage()
        {
            editButton = x => x.Marked("editButton");
            deleteButton = x => x.Marked("deleteButton");
        }

        public void TapEdit()
        {
            app.WaitForElement(editButton);
            app.Tap(editButton);
            app.Screenshot("Tapped edit button");
        }

        public void TapDelete()
        {
            app.WaitForElement(deleteButton);
            app.Tap(deleteButton);
            app.Screenshot("Tapped delete button");
        }
    }
}