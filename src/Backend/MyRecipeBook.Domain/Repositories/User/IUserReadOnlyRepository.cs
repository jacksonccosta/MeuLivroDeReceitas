namespace MyRecipeBook.Domain.Repositories;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
    public Task<User?> GetByEmailAndPassword(string email, string password);
    public Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
}
