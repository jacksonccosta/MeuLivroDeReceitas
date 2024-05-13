﻿using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain;
using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure;

public class UserRepository(MyRecipeBookDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext = dbContext;

    public async Task Add(Domain.User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
        return await _dbContext
                        .Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(user => user.Email.Equals(email) 
                                                     && user.Password.Equals(password) 
                                                     && user.Active);
    }

    public async Task<User> GetById(long id)
    {
        return await _dbContext
                        .Users
                        .FirstAsync(user => user.Id == id);
    }

    public void Update(User user) => _dbContext.Users.Update(user);
}
