namespace dotnet_5_rest_api_flash.Settings
{
   public class MongoDbSettings
   {
      public string Host { get; set; }
      public int Port { get; set; }
      public string User { get; set; }
      public string Password { get; set; }

      public string ConnectionString
      {
         get
         {
            return $"mongodb://{User}:{Password}@{Host}:{Port}";
         }
      }
   }
}