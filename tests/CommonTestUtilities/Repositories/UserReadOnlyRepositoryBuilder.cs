using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestUtilities;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();
    public IUserReadOnlyRepository Build() => _repository.Object;
    public void ExistActiveUserWithEmail(string email)
    {
        _repository.Setup(repo => repo.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }
}
