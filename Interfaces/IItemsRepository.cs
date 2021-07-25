using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_5_rest_api_flash.Entities;

namespace dotnet_5_rest_api_flash.Interfaces
{
   public interface IItemsRepository
   {
      Task<Item> GetItemAsync(Guid id);
      Task<IEnumerable<Item>> GetItemsAsync();
      Task CreateItemAsync(Item item);
      Task UpdateItemAsync(Item item);
      Task DeleteItemAsync(Guid id);
   }
}