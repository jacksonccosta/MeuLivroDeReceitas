using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain;

namespace MyRecipeBook.Infrastructure;

public class UnitOfWork(MyRecipeBookDbContext dbContext) : IUnitOfWork
{
    private readonly MyRecipeBookDbContext _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
