using InventoryManager.Context;
using InventoryManager.UserModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using static InventoryManager.Helper.Logger;
using InventoryManager.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace InventoryManager.Controllers
{
    public class InventoryController : ApiController
    {
        [HttpGet]
        [Route("~/api/Inventory/GetItems/{pageNumber}/{count}")]
        /// <summary>
        /// Get the list of products in the inventory
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="count">Number of items to be fetched</param>
        /// <param name="search">Search string</param>
        /// <returns>List of products in the inventory</returns>
        public async Task<List<InventoryDto>> GetItems(int pageNumber, int count, string search = null)
        {
            if (pageNumber <= 0 || count <= 0)
            {
                return null;
            }

            try
            {
                AppDbContext db = new AppDbContext();
                IEnumerable<Inventory> query = db.Items;

                // Search string
                if (string.IsNullOrEmpty(search) == false)
                {
                    query = query.Where(x => x.Name.Contains(search)
                                    || x.Description.Contains(search)
                                    || x.SerialNumber.Contains(search));
                }

                // Pagination
                List<Inventory> itemList = await Task.Run(() => query.Skip((pageNumber - 1) * count).Take(count).ToList());
                if (itemList.Count == 0)
                {
                    WriteLog(LogType.WARN, $"No items present for current conditions. pageNumber:{pageNumber} count:{count} search:{search}");
                    return null;
                }

                // Data binding
                List<InventoryDto> resultList = new List<InventoryDto>();
                itemList.ForEach(x => resultList.Add(new InventoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ManufactureDate = x.ManufacturingDate,
                    ExpiryDate = x.ExpiryDate
                }));

                WriteLog(LogType.INFO, "Successfully fetched items from inventory");
                return resultList;
            }
            catch (Exception ex)
            {
                WriteLog(LogType.ERROR, "Failed to list items from inventory", ex);
                return null;
            }
        }

        [HttpPost]
        [Route("~/api/Inventory/UpdateItem")]
        /// <summary>
        /// Update an item in the inventory
        /// </summary>
        /// <param name="item">Item details</param>
        /// <returns>
        /// true:   Success
        /// false:  Failure
        /// </returns>
        public async Task<bool> UpdateItem([FromBody] InventoryDto item)
        {
            // Parameter validation
            if (item == null || string.IsNullOrEmpty(item.Name) == true
                             || string.IsNullOrEmpty(item.Description) == true
                             || item.Price < 0
                             || item.ManufactureDate <= DateTime.MinValue
                             || item.ExpiryDate <= DateTime.MinValue)
            {
                return false;
            }

            try
            {
                // Get item from inventory
                AppDbContext db = new AppDbContext();
                Inventory dbItem = await Task.Run(() => db.Items.FirstOrDefault(x => x.Id == item.Id));
                if (dbItem == null)
                {
                    WriteLog(LogType.ERROR, "Item does not exist");
                    return false;
                }

                // Data binding
                dbItem.Name = item.Name;
                dbItem.Description = item.Description;
                dbItem.Price = item.Price;
                dbItem.ManufacturingDate = item.ManufactureDate;
                dbItem.ExpiryDate = item.ExpiryDate;

                // Updating in db
                await db.SaveChangesAsync();

                WriteLog(LogType.INFO, "Successfully updated item in inventory");
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(LogType.ERROR, "Failed to update item in inventory", ex);
                return false;
            }
        }

        [HttpPost]
        [Route("~/api/Inventory/AddItem")]
        /// <summary>
        /// Add an item into the inventory
        /// </summary>
        /// <param name="item">Item information</param>
        /// <returns>
        /// true:   Success
        /// false:  Failure
        /// </returns>
        public async Task<bool> AddItem([FromBody] InventoryDto item)
        {
            // Parameter validation
            if (item == null || string.IsNullOrEmpty(item.Name) == true
                             || string.IsNullOrEmpty(item.Description) == true
                             || item.Price < 0
                             || item.ManufactureDate <= DateTime.MinValue
                             || item.ExpiryDate <= DateTime.MinValue)
            {
                return false;
            }

            try
            {
                // Data binding
                Inventory insertItem = new Inventory()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ManufacturingDate = item.ManufactureDate,
                    ExpiryDate = item.ExpiryDate,
                    SerialNumber = GetSerialNumber()
                };

                // Adding in db
                AppDbContext db = new AppDbContext();
                db.Items.Add(insertItem);
                await db.SaveChangesAsync();

                WriteLog(LogType.INFO, "Successfully added item into inventory");
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(LogType.ERROR, "Failed to add item into inventory", ex);
                return false;
            }
        }

        [HttpPost]
        [Route("~/api/Inventory/DeleteItem/{id}")]
        /// <summary>
        /// Delete an item from inventory
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>
        /// true:   Success
        /// false:  Failure
        /// </returns>
        public async Task<bool> DeleteItem(int id)
        {
            // Parameter validation
            if (id <= 0)
            {
                return false;
            }

            try
            {
                // Get item from inventory
                AppDbContext db = new AppDbContext();
                Inventory item = await Task.Run(() => db.Items.FirstOrDefault(x => x.Id == id));
                if (item == null)
                {
                    WriteLog(LogType.INFO, "Item does not exist");
                    return true;
                }

                // Remove item from db
                db.Items.Remove(item);
                await db.SaveChangesAsync();

                WriteLog(LogType.INFO, "Successfully deleted item from inventory");
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(LogType.INFO, "Failed to delete item from inventory", ex);
                return false;
            }
        }

        /// <summary>
        /// Get serial number for the item
        /// </summary>
        /// <returns>Serial number</returns>
        private string GetSerialNumber()
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(characters, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
