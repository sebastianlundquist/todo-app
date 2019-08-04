using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace TestApp.UITest
{
    public class ItemsPage : BasePage
    {
        readonly Query addButton;
        readonly Query allListItems;
        readonly Func<int, Query> listItem;
        readonly Func<int, Query> listItemTitle;
        readonly Func<int, Query> listItemDescription;
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("itemListView")
        };

        public ItemsPage()
        {
            addButton = x => x.Marked("addButton");
            allListItems = x => x.All().Marked("itemContainer");
            listItem = i => x => x.Marked("itemContainer").Index(i);
            listItemTitle = i => x => x.Marked("titleLabel").Index(i);
            listItemDescription = i => x => x.Marked("descriptionLabel").Index(i);
        }

        public void TapAdd()
        {
            app.WaitForElement(addButton);
            app.Tap(addButton);
            app.Screenshot("Tapped list item");
        }

        public void TapItem(int index)
        {
            app.WaitForElement(listItem(index));
            app.Tap(listItem(index));
            app.Screenshot($"Tapped list item {index}");
        }

        public void VerifyNumberOfItems(int expected)
        {
            var result = app.Query(allListItems);
            Assert.AreEqual(expected, result.Length, $"Expected {expected} got {result.Length}");
        }

        public void CheckItem(int index, string expectedTitle, string expectedDescription)
        {
            var title = app.WaitForElement(listItemTitle(index));
            var description = app.WaitForElement(listItemDescription(index));
            Assert.AreEqual(expectedTitle, title, $"Unexpected item title");
            Assert.AreEqual(expectedDescription, description, $"Unexpected item description");
        }
    }
}