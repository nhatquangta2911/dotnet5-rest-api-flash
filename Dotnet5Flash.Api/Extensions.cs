using Dotnet5Flash.Api.Dtos;
using Dotnet5Flash.Api.Entities;

namespace Dotnet5Flash.Api
{
   public static class Extensions
   {
      public static ItemDto AsDto(this Item item)
      {
         return new ItemDto
         {
            ItemId = item.Id,
            ItemName = "[ITEM] " + item.Name,
            ItemPrice = item.Price,
            ItemCreatedDate = item.CreatedDate
         };
      }
   }
}