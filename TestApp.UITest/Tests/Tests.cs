using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestApp.UITest
{
    public class Tests : BaseTestFixture
    {
        public Tests(Platform platform) : base(platform) { }

        [Test]
        public void AddingItemUpdatesPage()
        {
            new ItemsPage()
                .TapAdd();

            new NewItemPage()
                .EnterTitle("Item title")
                .EnterDescription("Item description")
                .TapSave();

            new ItemsPage()
                .VerifyNumberOfItems(1);
        }

        [Test]
        public void CancellingReturnsToUnmodifiedItemsPage()
        {
            new ItemsPage()
                .TapAdd();

            new NewItemPage()
                .TapCancel();

            new ItemsPage()
                .VerifyNumberOfItems(0);
        }

        [Test]
        public void EditingItemUpdatesItemsPage()
        {
            new ItemsPage()
                .TapAdd();

            new NewItemPage()
                .EnterTitle("Item title")
                .EnterDescription("Item description")
                .TapSave();

            new ItemsPage()
                .TapItem(0);

            new ItemDetailPage()
                .TapEdit();

            new NewItemPage()
                .EnterTitle("New item title")
                .EnterDescription("New item description")
                .TapSave();

            new ItemsPage()
                .CheckItem(0, "New item title", "New item description");
        }
    }
}
