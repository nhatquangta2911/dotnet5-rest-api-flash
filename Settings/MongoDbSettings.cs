namespace dotnet_5_rest_api_flash.Settings
{
   public class MongoDbSettings
   {
      public string Host { get; set; }
      public int Port { get; set; }

      public string ConnectionString
      {
         get
         {
            return $"mongodb://{Host}:{Port}";
         }
      }
   }
}