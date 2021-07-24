using System;
using System.Collections.Generic;
using System.Linq;
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

      public IEnumerable<Item> GetItems()
      {
         return items;
      }

      public Item GetItem(Guid id)
      {
         return items.Where(item => item.Id == id).SingleOrDefault();
      }
   }
}