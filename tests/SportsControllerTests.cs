using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Marc.Mono.Service.Controllers;
using Marc.Mono.Service.Dtos;
using Marc.Mono.Service.Models;
using Marc.Mono.Service.Repositories;

public class SportsControllerTests
{
    private readonly Mock<ISportsRepository> _mockRepository;
    private readonly SportsController _controller;

    public SportsControllerTests()
    {
        _mockRepository = new Mock<ISportsRepository>();
        _controller = new SportsController(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateSportAsync_ValidInput_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var sportDto = new CreateSportDto
        {
            Name = "Football",
            ImageUri = "https://example.com/football.jpg",
            AdminId = Guid.NewGuid()
        };

        // Act
        var result = await _controller.CreateSportAsync(sportDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.IsAssignableFrom<SportDto>(createdAtActionResult.Value);
    }

    [Fact]
    public async Task CreateSportAsync_NullImageUri_SetsDefaultImageUri()
    {
        // Arrange
        var sportDto = new CreateSportDto
        {
            Name = "Tennis",
            AdminId = Guid.NewGuid()
        };

        // Act
        var result = await _controller.CreateSportAsync(sportDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var sport = Assert.IsAssignableFrom<SportDto>(createdAtActionResult.Value);
        Assert.Equal("https://placehold.co/100", sport.ImageUri);
    }
}
