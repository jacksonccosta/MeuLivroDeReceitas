using Moq;
using MyRecipeBook.Domain;
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

    public void GetByEmailAndPassword(User user)
    {
        _repository.Setup(repo => repo.GetByEmailAndPassword(user.Email, user.Password)).ReturnsAsync(user);
    }
}
