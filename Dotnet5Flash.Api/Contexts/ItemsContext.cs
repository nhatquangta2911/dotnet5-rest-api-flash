using System;
using Dotnet5Flash.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_5_rest_api_flash.Contexts
{
    public class ItemsContext : DbContext
    {
        public ItemsContext(DbContextOptions<ItemsContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
