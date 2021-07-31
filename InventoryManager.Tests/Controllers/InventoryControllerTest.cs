using InventoryManager.Controllers;
using InventoryManager.Helper;
using InventoryManager.UserModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManager.Tests.Controllers
{
    [TestClass]
    public class InventoryControllerTest
    {
        [TestMethod]
        public void A00_AddItem_Failure()
        {
            // Arrange
            InventoryController controller = new InventoryController();

            Assert.AreEqual(false, controller.AddItem(null).Result);

            InventoryDto item = new InventoryDto()
            {
                Name = "Eggs",
                Description = "White eggs",
                Price = 6,
                ManufactureDate = new DateTime(2021, 07, 29),
                ExpiryDate = new DateTime(2021, 08, 10)
            };

            InventoryDto item1 = item;
            item1.Name = string.Empty;
            Assert.AreEqual(false, controller.AddItem(item1).Result);
            item1.Name = null;
            Assert.AreEqual(false, controller.AddItem(item1).Result);

            item1 = item;
            item1.Description = string.Empty;
            Assert.AreEqual(false, controller.AddItem(item1).Result);
            item1.Description = null;
            Assert.AreEqual(false, controller.AddItem(item1).Result);

            item1 = item;
            item1.Price = -1;
            Assert.AreEqual(false, controller.AddItem(item1).Result);
            item1.Price = -9;
            Assert.AreEqual(false, controller.AddItem(item1).Result);

            item1 = item;
            item1.ManufactureDate = DateTime.MinValue;
            Assert.AreEqual(false, controller.AddItem(item1).Result);

            item1 = item;
            item1.ExpiryDate = DateTime.MinValue;
            Assert.AreEqual(false, controller.AddItem(item1).Result);
        }

        [TestMethod]
        public void A01_AddItem_Success()
        {
            // Arrange
            InventoryController controller = new InventoryController();

            InventoryDto item = new InventoryDto()
            {
                Name = "Eggs",
                Description = "White eggs",
                Price = 6,
                ManufactureDate = new DateTime(2021, 07, 29),
                ExpiryDate = new DateTime(2021, 08, 10)
            };

            var task1 = controller.AddItem(item);
            var task2 = controller.AddItem(item);
            var task3 = controller.AddItem(item);
            var task4 = controller.AddItem(item);
            var task5 = controller.AddItem(item);

            Assert.AreEqual(true, task1.Result);
            Assert.AreEqual(true, task2.Result);
            Assert.AreEqual(true, task3.Result);
            Assert.AreEqual(true, task4.Result);
            Assert.AreEqual(true, task5.Result);
        }

        [TestMethod]
        public void A02_UpdateItem_Failure()
        {
            // Arrange
            InventoryController controller = new InventoryController();

            Assert.AreEqual(false, controller.UpdateItem(null).Result);

            InventoryDto item = new InventoryDto()
            {
                Id = 5,
                Name = "Eggs",
                Description = "White eggs",
                Price = 6,
                ManufactureDate = new DateTime(2021, 07, 29),
                ExpiryDate = new DateTime(2021, 08, 10)
            };

            InventoryDto item1 = item;
            item1.Id = -2;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);

            item1 = item;
            item1.Name = string.Empty;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);
            item1.Name = null;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);

            item1 = item;
            item1.Description = string.Empty;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);
            item1.Description = null;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);

            item1 = item;
            item1.Price = -1;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);
            item1.Price = -9;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);

            item1 = item;
            item1.ManufactureDate = DateTime.MinValue;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);

            item1 = item;
            item1.ExpiryDate = DateTime.MinValue;
            Assert.AreEqual(false, controller.UpdateItem(item1).Result);
        }

        [TestMethod]
        public void A03_UpdateItem_Success()
        {
            // Arrange
            InventoryController controller = new InventoryController();

            InventoryDto item = new InventoryDto()
            {
                Id = 5,
                Name = "Eggs",
                Description = "White eggs",
                Price = 6,
                ManufactureDate = new DateTime(2021, 07, 29),
                ExpiryDate = new DateTime(2021, 08, 10)
            };

            Assert.AreEqual(true, controller.UpdateItem(item).Result);
        }

        [TestMethod]
        // [Inline]
        public void A04_GetItems_Failure()
        {
            // Arrange
            InventoryController controller = new InventoryController();

            Assert.AreEqual(null, controller.GetItems(0, 10, "stop").Result);
            Assert.AreEqual(null, controller.GetItems(-1, 10, "stop").Result);
            Assert.AreEqual(null, controller.GetItems(1, 0, "stop").Result);
            Assert.AreEqual(null, controller.GetItems(1, -2, "stop").Result);
            Assert.AreEqual(null, controller.GetItems(0, 10).Result);
            Assert.AreEqual(null, controller.GetItems(-1, 10).Result);
            Assert.AreEqual(null, controller.GetItems(1, 0).Result);
            Assert.AreEqual(null, controller.GetItems(1, -2).Result);
            Assert.AreEqual(null, controller.GetItems(1, 10, "stop").Result);
        }

        [TestMethod]
        public void A05_GetItems_Failure()
        {
            // Arrange
            InventoryController controller = new InventoryController();

            Assert.IsNotNull(controller.GetItems(1, 10).Result);
            Assert.IsNotNull(controller.GetItems(1, 10, "Milk").Result);
        }
    }
}
