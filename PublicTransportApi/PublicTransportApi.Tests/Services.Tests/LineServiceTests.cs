using NUnit.Framework;
using PublicTransportApi.Services;
using PublicTransportApi.Services.Interfaces;
using PublicTransportApi.Tests.TestHelpers;
using PublicTransportApi.Validators;
using FluentAssertions;

namespace PublicTransportApi.Tests.Services.Tests;

[TestFixture]
public class LineServiceTests
{
    private DbContextHelper _dbContextHelper = null!;
    private ILineService _lineService = null!; 
    
    [SetUp]
    public void SetUp()
    {
        _dbContextHelper = new DbContextHelper().WithTestLineEntries();
        var dbContext = _dbContextHelper.GetDbContext();
        _lineService = new LineService(dbContext, new LineValidator());
    }

    [Test]
    public async Task GetLine_ValidId_ReturnsLine()
    {
        // Arrange
        const int lineId = 1;
        
        // Act
        var actualLine = await _lineService.GetLine(lineId);

        // Assert
        actualLine.Should().NotBeNull();
        actualLine.IsSuccess.Should().BeTrue();
        actualLine.Message.Should().NotBeEmpty();
        actualLine.Data.Should().NotBeNull();
    }

    [Test]
    public async Task GetLine_ValidId_ReturnsLineWithCorrectId()
    {
        // Arrange
        const int lineId = 1;
        
        // Act
        var actualLine = await _lineService.GetLine(lineId);

        // Assert
        actualLine.Should().NotBeNull();
        actualLine.Data.Should().NotBeNull();
        actualLine.Data.Id.Should().Be(lineId);
    }
    
    [Test]
    public async Task GetLine_ValidId_ReturnsLineWithCorrectIdentifier()
    {
        // Arrange
        const int lineId = 1;
        var defaultLine = DbContextHelper.GenerateLine();
        var expectedIdentifier = defaultLine.Identifier;
        
        // Act
        var actualLine = await _lineService.GetLine(lineId);

        // Assert
        actualLine.Should().NotBeNull();
        actualLine.Data.Should().NotBeNull();
        actualLine.Data.Identifier.Should().Be(expectedIdentifier);
    }
    
    [Test]
    public async Task GetLine_ValidId_ReturnsLineWithCorrectName()
    {
        // Arrange
        const int lineId = 1;
        var defaultLine = DbContextHelper.GenerateLine();
        var expectedName = defaultLine.Name;
        
        // Act
        var actualLine = await _lineService.GetLine(lineId);

        // Assert
        actualLine.Should().NotBeNull();
        actualLine.Data.Should().NotBeNull();
        actualLine.Data.Name.Should().Be(expectedName);
    }
}