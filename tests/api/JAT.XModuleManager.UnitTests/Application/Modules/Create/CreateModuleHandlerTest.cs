using JAT.XModuleManager.Application.Modules.Create;
using JAT.XModuleManager.Domain;
using JAT.XModuleManager.Domain.Interfaces;
using Moq;

namespace JAT.XModuleManager.UnitTests.Application.Modules.Create;

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
        _moduleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Module>(), cancellationToken), Times.Never);
        Assert.NotNull(result);
        Assert.IsType<CreateModuleResult>(result);
    }
}