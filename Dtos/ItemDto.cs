using System;

namespace dotnet_5_rest_api_flash.Dtos
{
   public record ItemDto
   {
      public Guid ItemId { get; init; }
      public string ItemName { get; init; }
      public decimal ItemPrice { get; init; }
      public DateTimeOffset ItemCreatedDate { get; init; }
   }
}