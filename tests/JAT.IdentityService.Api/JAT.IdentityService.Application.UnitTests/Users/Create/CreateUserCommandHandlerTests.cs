using JAT.IdentityService.Application.Users.Create;
using Moq;
using JAT.IdentityService.Domain.Users;
using System.Linq.Expressions;
using JAT.Core.Domain.Interfaces;
using JAT.IdentityService.Domain.Interfaces.Services;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using JAT.Core.Domain.Enums;

namespace JAT.IdentityService.Application.UnitTests.Users.Create;

public class CreateUserCommandHandlerTestsTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTestsTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _handler = new CreateUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _passwordServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnResult()
    {
        // Arrange
        var command = new CreateUserCommand("Test User", "test@example.com", "password123");
        var cancellationToken = CancellationToken.None;
        var hashedPassword = "hashedPassword";
        var salt = new byte[] { 1, 2, 3, 4, 5 };

        _userRepositoryMock
            .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>(), cancellationToken))
            .ReturnsAsync(false);

        _passwordServiceMock
            .Setup(service => service.GenerateSalt())
            .Returns(salt);

        _passwordServiceMock
            .Setup(service => service.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Returns(hashedPassword);

        Expression<Func<User, bool>> userValidExpression = user => user.Id != Guid.Empty
            && user.Username == command.Username
            && user.Email == command.Email
            && user.PasswordHash == hashedPassword
            && user.Role == UserRoleType.User
            && user.Salt != null
            && user.Status == UserStatusType.Active;

        _userRepositoryMock
            .Setup(repo => repo.AddAsync(It.Is(userValidExpression), cancellationToken))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(uow => uow.CommitAsync(cancellationToken))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _userRepositoryMock.Verify(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>(), cancellationToken), Times.Once);
        _passwordServiceMock.Verify(service => service.GenerateSalt(), Times.Once);
        _passwordServiceMock.Verify(service => service.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), cancellationToken), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(cancellationToken), Times.Once);
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.IsType<CreateUserResult>(result.Value);
        Assert.NotEqual(result.Value.Id, Guid.Empty);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserAlreadyExists()
    {
        // Arrange
        var command = new CreateUserCommand("Test User", "test@example.com", "password123");
        var cancellationToken = CancellationToken.None;

        _userRepositoryMock
            .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>(), cancellationToken))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _userRepositoryMock.Verify(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>(), cancellationToken), Times.Once);
        Assert.True(result.IsError);
        Assert.Null(result.Value);
        var error = Assert.Single(result.Errors);
        Assert.Equal(UserError.UserAlreadyExists, error);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCommitAsyncFails()
    {
        // Arrange
        var command = new CreateUserCommand("Test User", "test@example.com", "password123");
        var cancellationToken = CancellationToken.None;
        var hashedPassword = "hashedPassword";
        var salt = new byte[] { 1, 2, 3, 4, 5 };

        _userRepositoryMock
            .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>(), cancellationToken))
            .ReturnsAsync(false);

        _passwordServiceMock
            .Setup(service => service.GenerateSalt())
            .Returns(salt);

        _passwordServiceMock
            .Setup(service => service.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Returns(hashedPassword);

        _userRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<User>(), cancellationToken))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(uow => uow.CommitAsync(cancellationToken))
            .ReturnsAsync(false); // Simulate commit failure

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _userRepositoryMock.Verify(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>(), cancellationToken), Times.Once);
        _passwordServiceMock.Verify(service => service.GenerateSalt(), Times.Once);
        _passwordServiceMock.Verify(service => service.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), cancellationToken), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(cancellationToken), Times.Once);
        Assert.True(result.IsError);
        Assert.Null(result.Value);
        var error = Assert.Single(result.Errors);
        Assert.Equal(UserError.UserCouldNotBeCreated, error);
    }
}