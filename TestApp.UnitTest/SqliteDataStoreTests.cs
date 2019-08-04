using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TestApp.Services;

namespace TestApp.UnitTest
{
    [TestClass]
    public class SqliteDataStoreTests
    {
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(100)]
        public void DatabaseContainsCorrectNumberOfItems(int numberOfItems)
        {
            var database = new SqliteDataStore("./db");
            for (int i = 0; i < numberOfItems; i++)
            {
                database.SaveItem(new Models.Item()
                {
                    Title = "Title",
                    Description = "Description",
                    SetReminder = "true",
                    ReminderTime = System.DateTime.Now
                });
            }
            int result = database.GetItems().Count();
            database.DeleteAllItems();
            Assert.AreEqual(numberOfItems, result, $"Expected {numberOfItems} got {result}");
        }
    }
}
