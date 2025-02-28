using JAT.Modules.Application.Modules.Create;
using JAT.Modules.Domain;
using JAT.Modules.Domain.Interfaces;
using Moq;

namespace JAT.Modules.Application.UnitTests.Modules.Create;

public class CreateModuleHandlerTest
{
    private readonly Mock<IModuleRepository> _moduleRepositoryMock;
    private readonly CreateModuleHandler _handler;

    public CreateModuleHandlerTest()
    {
        _moduleRepositoryMock = new Mock<IModuleRepository>();
        _handler = new CreateModuleHandler(_moduleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateModuleAndReturnResult()
    {
        // Arrange
        var command = new CreateModuleCommand("Test Module", "Test Description", "Test Type");
        var cancellationToken = CancellationToken.None;

        _moduleRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Module>(), cancellationToken))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _moduleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Module>(), cancellationToken), Times.Once);
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.IsType<CreateModuleResult>(result);
    }
}