using System;
using System.Threading.Tasks;
using Dotnet5Flash.Api.Controllers;
using Dotnet5Flash.Api.Entities;
using Dotnet5Flash.Api.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Dotnet5Flash.UnitTests
{
   public class ItemsControllerTests
   {
      private readonly Mock<IItemsRepository> repositoryStub = new();
      private readonly Mock<ILogger<ItemsController>> loggerStub = new();
      private readonly Random random = new();

      [Fact]
      public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
      {
         // Arrange
         repositoryStub.Setup(repository => repository.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);

         var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

         // Act
         var result = await controller.GetItemAsync(Guid.NewGuid());

         // Assert
         Assert.IsType<NotFoundResult>(result.Result);
      }

      [Fact]
      public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
      {
         // Arrange
         Item expectedItem = CreateRandomItem();

         repositoryStub.Setup(repository => repository.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync(expectedItem);

         var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

         // Act
         var result = await controller.GetItemAsync(Guid.NewGuid());

         // Assert
         Assert.IsType<Item>(expectedItem);
      }

      private Item CreateRandomItem()
      {
         return new()
         {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Price = random.Next(1000),
            CreatedDate = DateTimeOffset.UtcNow
         };
      }
   }
}
