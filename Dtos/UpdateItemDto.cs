using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_5_rest_api_flash.Dtos
{
   public record UpdateItemDto
   {
      [Required]
      public string Name { get; init; }

      [Required]
      [Range(1, 1000)]
      public decimal Price { get; init; }
   }
}