using System;
using System.Collections.Generic;
using dotnet_5_rest_api_flash.Entities;

namespace dotnet_5_rest_api_flash.Interfaces
{
   public interface IItemsRepository
   {
      Item GetItem(Guid id);
      IEnumerable<Item> GetItems();
      void CreateItem(Item item);
      void UpdateItem(Item item);
      void DeleteItem(Guid id);
   }
}