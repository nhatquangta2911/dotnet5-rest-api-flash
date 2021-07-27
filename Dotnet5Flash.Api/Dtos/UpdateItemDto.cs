using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet5Flash.Api.Dtos
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