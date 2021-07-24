using System;
using System.Collections.Generic;
using dotnet_5_rest_api_flash.Entities;
using dotnet_5_rest_api_flash.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_5_rest_api_flash.Controllers
{
   [ApiController]
   [Route("items")]
   public class ItemsController : ControllerBase
   {
      private readonly InMemoryItemsRepository repository;

      public ItemsController()
      {
         repository = new InMemoryItemsRepository();
      }

      [HttpGet]
      public IEnumerable<Item> GetItems()
      {
         var items = repository.GetItems();
         return items;
      }

      [HttpGet("{id}")]
      public ActionResult<Item> GetItem(Guid id)
      {
         var item = repository.GetItem(id);

         if (item is null)
         {
            return NotFound();
         }

         return Ok(item);
      }
   }
}