using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TestApp.Models;
using TestApp.Services;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.UnitTest
{
    [TestClass]
    public class ItemDetailViewModelTests
    {
        [TestMethod]
        public void DeleteItem_Correctly_Deletes_One_Item()
        {
            var mockDb = new Mock<IDataStore<Item>>();
            var item = new Item();
            var vm = new ItemDetailViewModel(mockDb.Object, new Item());
            var items = new List<Item>() { item };

            mockDb.Setup(x => x.DeleteItem(item)).Callback(() => items.Remove(item));

            vm.DeleteItem();

            Assert.AreEqual(0, items.Count);
        }
    }
}
