using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_5_rest_api_flash.Constants;
using dotnet_5_rest_api_flash.Dtos;
using dotnet_5_rest_api_flash.Entities;
using dotnet_5_rest_api_flash.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_5_rest_api_flash.Controllers
{
   [ApiController]
   [Route(RouteConstants.Items)]
   public class ItemsController : ControllerBase
   {
      private readonly IItemsRepository repository;

      public ItemsController(IItemsRepository repository)
      {
         this.repository = repository;
      }

      [HttpGet]
      public async Task<IEnumerable<ItemDto>> GetItemsAsync()
      {
         var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
         return items;
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
      {
         var item = await repository.GetItemAsync(id);

         if (item is null)
         {
            return NotFound();
         }

         return Ok(item.AsDto());
      }

      [HttpPost]
      public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
      {
         Item item = new()
         {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
         };
         await repository.CreateItemAsync(item);
         return CreatedAtAction(nameof(GetItemAsync), new { Id = item.Id }, item.AsDto());
      }

      [HttpPut("{id}")]
      public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDtio)
      {
         var existingItem = await repository.GetItemAsync(id);

         if (existingItem is null)
         {
            return NotFound();
         }

         Item updatedItem = existingItem with
         {
            Name = itemDtio.Name,
            Price = itemDtio.Price
         };

         await repository.UpdateItemAsync(updatedItem);

         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult> DeleteItem(Guid id)
      {
         var existingItem = await repository.GetItemAsync(id);

         if (existingItem is null)
         {
            return NotFound();
         }

         await repository.DeleteItemAsync(id);

         return NoContent();
      }
   }
}