using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_5_rest_api_flash.Contexts;
using Dotnet5Flash.Api.Entities;
using Dotnet5Flash.Api.Interfaces;

namespace dotnet_5_rest_api_flash.Repositories
{
    public class SqlServerItemsRepository : IItemsRepository
    {
        private ItemsContext _itemsContext;

        public SqlServerItemsRepository(ItemsContext itemsContext)
        {
            _itemsContext = itemsContext;
        }

        public async Task CreateItemAsync(Item item)
        {
            item.Id = Guid.NewGuid();
            _itemsContext.Items.Add(item);
            _itemsContext.SaveChanges();
            await Task.FromResult(item);
        }

        public Task DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = _itemsContext.Items.Find(id);
            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var items = _itemsContext.Items.ToList();
            return await Task.FromResult(items);
        }

        public Task UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
