using NUnit.Framework;
using PublicTransportApi.Services;
using PublicTransportApi.Services.Interfaces;
using PublicTransportApi.Tests.TestHelpers;
using PublicTransportApi.Validators;
using FluentAssertions;
using PublicTransportApi.Data;

namespace PublicTransportApi.Tests.Services.Tests;

[TestFixture]
public class LineServiceTests
{
    private DbContextHelper _dbContextHelper = null!;
    private ApplicationDbContext _applicationDbContext = null!;
    private ILineService _lineService = null!;
    
    [SetUp]
    public void SetUp()
    {
        _dbContextHelper = new DbContextHelper().WithTestLineEntries();
        _applicationDbContext = _dbContextHelper.GetDbContext();
        _lineService = new LineService(_applicationDbContext, new LineValidator());
    }

    [TearDown]
    public void TearDown()
    {
        _applicationDbContext.Dispose();
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
    
    [Test]
    public async Task GetLine_InvalidId_ReturnResultError()
    {
        // Arrange
        const int lineId = 999;
        
        // Act
        var actualLine = await _lineService.GetLine(lineId);

        // Assert
        actualLine.Should().NotBeNull();
        actualLine.IsSuccess.Should().BeFalse();
        actualLine.Data.Should().BeNull();
    }
    
    [Test]
    public async Task GetLineByIdentifier_ValidIdentifier_ReturnsLineWithCorrectIdentifier()
    {
        // Arrange
        var defaultLine = DbContextHelper.GenerateLine();
        var expectedIdentifier = defaultLine.Identifier;
        
        // Act
        var actualLine = await _lineService.GetLineByIdentifier(expectedIdentifier!);

        // Assert
        actualLine.Should().NotBeNull();
        actualLine.Data.Should().NotBeNull();
        actualLine.Data.Identifier.Should().Be(expectedIdentifier);
    }
    
    [Test]
    public async Task GetLineByIdentifier_InvalidIdentifier_ReturnsResultError()
    {
        // Arrange
        const string invalidIdentifier = "INVALID";
        
        // Act
        var actualLine = await _lineService.GetLineByIdentifier(invalidIdentifier);

        // Assert
        actualLine.Should().NotBeNull();
        actualLine.IsSuccess.Should().BeFalse();
        actualLine.Data.Should().BeNull();
    }
}