using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_5_rest_api_flash.Constants;
using dotnet_5_rest_api_flash.Entities;
using dotnet_5_rest_api_flash.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace dotnet_5_rest_api_flash.Repositories
{
   public class MongoDbItemsRepository : IItemsRepository
   {

      private readonly IMongoCollection<Item> itemsCollection;
      private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

      public MongoDbItemsRepository(IMongoClient mongoClient)
      {
         IMongoDatabase database = mongoClient.GetDatabase(DatabaseConstants.DatabaseName);
         itemsCollection = database.GetCollection<Item>(DatabaseConstants.CollectionName);
      }

      public async Task CreateItemAsync(Item item)
      {
         await itemsCollection.InsertOneAsync(item);
      }

      public async Task DeleteItemAsync(Guid id)
      {
         var filter = filterBuilder.Eq(item => item.Id, id);
         await itemsCollection.DeleteOneAsync(filter);
      }

      public async Task<Item> GetItemAsync(Guid id)
      {
         var filter = filterBuilder.Eq(item => item.Id, id);
         return await itemsCollection.Find(filter).SingleOrDefaultAsync();
      }

      public async Task<IEnumerable<Item>> GetItemsAsync()
      {
         return await itemsCollection.Find(new BsonDocument()).ToListAsync();
      }

      public async Task UpdateItemAsync(Item item)
      {
         var filter = filterBuilder.Eq(item => item.Id, item.Id);
         await itemsCollection.ReplaceOneAsync(filter, item);
      }
   }
}