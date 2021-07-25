using System;
using System.Collections.Generic;
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

      public void CreateItem(Item item)
      {
         itemsCollection.InsertOne(item);
      }

      public void DeleteItem(Guid id)
      {
         var filter = filterBuilder.Eq(item => item.Id, id);
         itemsCollection.DeleteOne(filter);
      }

      public Item GetItem(Guid id)
      {
         var filter = filterBuilder.Eq(item => item.Id, id);
         return itemsCollection.Find(filter).SingleOrDefault();
      }

      public IEnumerable<Item> GetItems()
      {
         return itemsCollection.Find(new BsonDocument()).ToList();
      }

      public void UpdateItem(Item item)
      {
         var filter = filterBuilder.Eq(item => item.Id, item.Id);
         itemsCollection.ReplaceOne(filter, item);
      }
   }
}