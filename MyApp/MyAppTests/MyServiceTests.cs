using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyApp;
using MyApp.Wrappers;
using MyAppTests.Helpers;

namespace MyApp.Tests
{
    [TestClass]
    public class MyServiceTests
    {
        [TestMethod]
        public void AddItemsTest()
        {
            // Arrange: set up a context mock with an empty collection of root items, exposed through a provider mock.
            Mock<IMyContextWrapper> myContextMock = new Mock<IMyContextWrapper>();
            var myRootItems = new DbSetMock<MyRootItem>();
            myContextMock.Setup(m => m.MyRootItems).Returns(myRootItems);
            Mock<IMyContextProvider> myContextProvider = new Mock<IMyContextProvider>();
            myContextProvider.Setup(m => m.CreateContext()).Returns(myContextMock.Object);

            // Act: create the service instance and call AddItems operation.
            var service = new MyService(myContextProvider.Object);
            service.AddItems();

            // Assert: verify that the context is created, and that the side effects are as expected.
            myContextProvider.Verify(m => m.CreateContext());
            Assert.AreEqual(2, myRootItems.Count());
        }

        [TestMethod]
        public void GetItemsTest()
        {
            // Arrange: set up a context mock with an initial collection of root items, exposed through a provider mock.
            Mock<IMyContextWrapper> myContextMock = new Mock<IMyContextWrapper>();
            var myRootItems = new DbSetMock<MyRootItem>(MyMockedRootItems);
            myContextMock.Setup(m => m.MyRootItems).Returns(myRootItems);
            Mock<IMyContextProvider> myContextProvider = new Mock<IMyContextProvider>();
            myContextProvider.Setup(m => m.CreateContext()).Returns(myContextMock.Object);

            // Act: create the service instance and call GetItems operation, preserving the result.
            var service = new MyService(myContextProvider.Object);
            var result = service.GetItems();

            // Assert: verify that the context is created, and that the result is correct.
            myContextProvider.Verify(m => m.CreateContext());
            Assert.AreEqual(myRootItems.Count(), result.Count());
        }

        [TestMethod]
        public void UpdateAllChildItemValuesTest()
        {
            // Arrange: set up a context mock with an initial collection of child items (obtained from mocked root items), exposed through a provider mock.
            Mock<IMyContextWrapper> myContextMock = new Mock<IMyContextWrapper>();
            var myChildItems = new DbSetMock<MyChildItem>(MyMockedRootItems.SelectMany(r => r.MyChildItems));
            myContextMock.Setup(m => m.MyChildItems).Returns(myChildItems);
            Mock<IMyContextProvider> myContextProvider = new Mock<IMyContextProvider>();
            myContextProvider.Setup(m => m.CreateContext()).Returns(myContextMock.Object);

            // Act: create the service instance and call UpdateAllChildItemValues operation with a specific argument.
            var service = new MyService(myContextProvider.Object);
            const string postfix = "(postfix)";
            service.UpdateAllChildItemValues(postfix);

            // Assert: verify that the context is created, and that the side effects are as expected.
            myContextProvider.Verify(m => m.CreateContext());
            Assert.IsTrue(myChildItems.All(c => c.MyChildValue.EndsWith($" {postfix}")));
        }

        [TestMethod]
        public void RemoveItemsTest()
        {
            // Arrange: set up a context mock with an initial collection of root items, exposed through a provider mock.
            Mock<IMyContextWrapper> myContextMock = new Mock<IMyContextWrapper>();
            var myRootItems = new DbSetMock<MyRootItem>(MyMockedRootItems);
            myContextMock.Setup(m => m.MyRootItems).Returns(myRootItems);
            Mock<IMyContextProvider> myContextProvider = new Mock<IMyContextProvider>();
            myContextProvider.Setup(m => m.CreateContext()).Returns(myContextMock.Object);

            // Act: create the service instance and call RemoveItems operation.
            var service = new MyService(myContextProvider.Object);
            service.RemoveItems();

            // Assert: verify that the context is created, and that the side effects are as expected.
            myContextProvider.Verify(m => m.CreateContext());
            Assert.AreEqual(0, myRootItems.Count());
        }

        // Holds a predefined list of mocked root items to use during tests instead of a database with existing data.
        private static readonly List<MyRootItem> MyMockedRootItems = new List<MyRootItem>
        {
            new MyRootItem
            {
                Id = 1,
                MyRootValue = "Mock A",
                MyChildItems = new List<MyChildItem>
                {
                    new MyChildItem { Id = 1, MyChildValue = "Mock A.1" },
                    new MyChildItem { Id = 2, MyChildValue = "Mock A.2" }
                }
            },
            new MyRootItem
            {
                Id = 2,
                MyRootValue = "Mock B",
                MyChildItems = new List<MyChildItem>
                {
                    new MyChildItem { Id = 3, MyChildValue = "Mock B.1" }
                }
            }
        };
    }
}
