namespace MyRecipeBook.Domain.Repositories;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
}
