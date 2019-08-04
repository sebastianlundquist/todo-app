using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TestApp.Models;
using TestApp.Services;
using TestApp.ViewModels;

namespace TestApp.UnitTest
{
    [TestClass]
    public class ItemsViewModelTests
    {
        [TestMethod]
        public void LoadItems_Correctly_Modifies_Items_Property()
        {
            var mockDb = new Mock<IDataStore<Item>>();
            var vm = new ItemsViewModel(mockDb.Object);
            var items = new List<Item>();
            for (int i = 0; i < 5; i++)
                items.Add(new Item());
            mockDb.Setup(x => x.GetItems()).Returns(items);

            vm.LoadItems();

            Assert.AreEqual(5, vm.Items.Count);
        }
    }
}
