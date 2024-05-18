namespace MyRecipeBook.Domain;

public interface IUserUpdateOnlyRepository
{
    public Task<User> GetById(long id);
    public void Update(User user);
}
