using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_5_rest_api_flash.Entities;
using dotnet_5_rest_api_flash.Interfaces;

namespace dotnet_5_rest_api_flash.Repositories
{
   public class InMemoryItemsRepository : IItemsRepository
   {
      private readonly List<Item> items = new()
      {
         new Item { Id = Guid.NewGuid(), Name = "Notebook A1", Price = 5, CreatedDate = DateTimeOffset.UtcNow },
         new Item { Id = Guid.NewGuid(), Name = "Gel Pen 0.4", Price = 7, CreatedDate = DateTimeOffset.UtcNow },
         new Item { Id = Guid.NewGuid(), Name = "Sticky Notes", Price = 2, CreatedDate = DateTimeOffset.UtcNow },
         new Item { Id = Guid.NewGuid(), Name = "Pencil B1", Price = 1.5M, CreatedDate = DateTimeOffset.UtcNow },
         new Item { Id = Guid.NewGuid(), Name = "White Board 50x30", Price = 6, CreatedDate = DateTimeOffset.UtcNow }
      };

      public async Task<IEnumerable<Item>> GetItemsAsync()
      {
         return await Task.FromResult(items);
      }

      public async Task<Item> GetItemAsync(Guid id)
      {
         return await Task.FromResult(items.Where(item => item.Id == id).SingleOrDefault());
      }

      public async Task CreateItemAsync(Item item)
      {
         items.Add(item);
         await Task.CompletedTask;
      }

      public async Task UpdateItemAsync(Item item)
      {
         var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
         items[index] = item;
         await Task.CompletedTask;
      }

      public async Task DeleteItemAsync(Guid id)
      {
         var index = items.FindIndex(existingItem => existingItem.Id == id);
         items.RemoveAt(index);
         await Task.CompletedTask;
      }
   }
}