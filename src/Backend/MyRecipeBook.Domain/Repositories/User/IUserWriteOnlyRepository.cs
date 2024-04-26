namespace MyRecipeBook.Domain.Repositories;

public interface IUserWriteOnlyRepository
{
    public Task Add(User user);
}
