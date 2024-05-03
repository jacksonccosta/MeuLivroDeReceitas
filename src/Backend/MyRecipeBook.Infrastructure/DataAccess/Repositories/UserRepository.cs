using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain;
using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure;

public class UserRepository(MyRecipeBookDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext = dbContext;

    public async Task Add(Domain.User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        return await _dbContext
                        .Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(user => user.Email.Equals(email) 
                                                     && user.Password.Equals(password) 
                                                     && user.Active);
    }
}
