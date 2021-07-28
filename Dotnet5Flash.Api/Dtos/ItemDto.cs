using System;

namespace Dotnet5Flash.Api.Dtos
{
   public record ItemDto
   {
      public Guid ItemId { get; init; }
      public string ItemName { get; init; }
      public decimal ItemPrice { get; init; }
      public DateTimeOffset ItemCreatedDate { get; init; }
   }
}