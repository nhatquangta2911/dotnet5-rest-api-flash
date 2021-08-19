using System;

namespace Dotnet5Flash.Api.Dtos
{
    public record ItemDto
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public DateTimeOffset ItemCreatedDate { get; set; }
    }
}