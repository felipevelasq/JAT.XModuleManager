using JAT.IdentityService.Application.Users.List;
using Moq;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using JAT.IdentityService.Domain.Users;
using JAT.Core.Domain.Enums;

namespace JAT.IdentityService.Application.UnitTests.Users.List
{
    public class ListUsersQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly ListUsersQueryHandler _handler;

        public ListUsersQueryHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new ListUsersQueryHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfUsers()
        {
            // Arrange
            var query = new ListUsersQuery();
            var cancellationToken = CancellationToken.None;
            var users = new List<User>
            {
                new(Guid.NewGuid(),
                "Test User",
                "test@example.com",
                "hashedPassword",
                UserRoleType.User,
                [1, 2, 3, 4, 5],
                UserStatusType.Active)
            };

            _userRepositoryMock
                .Setup(repo => repo.GetAllAsync(cancellationToken))
                .ReturnsAsync(users);

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
            Assert.False(result.IsError);
            Assert.NotNull(result.Value);

            var expectedUsers = users.Select(user => new UserDTO(
                user.Id,
                user.Username,
                user.Email,
                user.Role,
                user.Status));

            Assert.Equal(expectedUsers, result.Value);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyListWhenNoUsersFound()
        {
            // Arrange
            var query = new ListUsersQuery();
            var cancellationToken = CancellationToken.None;
            var users = new List<User>();

            _userRepositoryMock
                .Setup(repo => repo.GetAllAsync(cancellationToken))
                .ReturnsAsync(users);

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
            Assert.False(result.IsError);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);
        }
    }
}