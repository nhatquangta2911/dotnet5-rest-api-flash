using dotnet_5_rest_api_flash.Dtos;
using dotnet_5_rest_api_flash.Entities;

namespace dotnet_5_rest_api_flash
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