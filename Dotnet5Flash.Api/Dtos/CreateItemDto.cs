using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet5Flash.Api.Dtos
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}