using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_5_rest_api_flash.Dtos;
using dotnet_5_rest_api_flash.Entities;
using dotnet_5_rest_api_flash.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_5_rest_api_flash.Controllers
{
   [ApiController]
   [Route("items")]
   public class ItemsController : ControllerBase
   {
      private readonly IItemsRepository repository;

      public ItemsController(IItemsRepository repository)
      {
         this.repository = repository;
      }

      [HttpGet]
      public IEnumerable<ItemDto> GetItems()
      {
         var items = repository.GetItems().Select(item => item.AsDto());
         return items;
      }

      [HttpGet("{id}")]
      public ActionResult<ItemDto> GetItem(Guid id)
      {
         var item = repository.GetItem(id);

         if (item is null)
         {
            return NotFound();
         }

         return Ok(item.AsDto());
      }

      [HttpPost]
      public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
      {
         Item item = new()
         {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
         };
         repository.CreateItem(item);
         return CreatedAtAction(nameof(GetItem), new { Id = item.Id }, item.AsDto());
      }

      [HttpPut("{id}")]
      public ActionResult UpdateItem(Guid id, UpdateItemDto itemDtio)
      {
         var existingItem = repository.GetItem(id);

         if (existingItem is null)
         {
            return NotFound();
         }

         Item updatedItem = existingItem with
         {
            Name = itemDtio.Name,
            Price = itemDtio.Price
         };

         repository.UpdateItem(updatedItem);

         return NoContent();
      }

      [HttpDelete("{id}")]
      public ActionResult DeleteItem(Guid id)
      {
         var existingItem = repository.GetItem(id);

         if (existingItem is null)
         {
            return NotFound();
         }

         repository.DeleteItem(id);

         return NoContent();
      }
   }
}