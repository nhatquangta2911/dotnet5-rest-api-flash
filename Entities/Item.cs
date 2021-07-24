using System;

namespace dotnet_5_rest_api_flash.Entities
{
   public record Item
   {
      public Guid Id { get; init; }
      public string Name { get; init; }
      public decimal Price { get; init; }
      public DateTimeOffset CreatedDate { get; init; }
   }
}