using System;

namespace Dotnet5Flash.Api.Entities
{
   public record Item
   {
      public Guid Id { get; init; }
      public string Name { get; init; }
      public decimal Price { get; init; }
      public DateTimeOffset CreatedDate { get; init; }
   }
}