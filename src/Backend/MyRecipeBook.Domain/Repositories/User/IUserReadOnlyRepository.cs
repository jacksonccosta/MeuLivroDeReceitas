namespace MyRecipeBook.Domain.Repositories;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
    public Task<User?> GetUserByEmailAndPassword(string email, string password);
}
