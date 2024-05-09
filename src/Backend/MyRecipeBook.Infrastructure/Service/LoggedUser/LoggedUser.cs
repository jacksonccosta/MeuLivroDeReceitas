using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain;
using MyRecipeBook.Domain.Security;
using MyRecipeBook.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyRecipeBook.Infrastructure.Service;

public class LoggedUser(MyRecipeBookDbContext dbContext, ITokenProvider token) : ILoggedUser
{
    private readonly MyRecipeBookDbContext _dbContext = dbContext;
    private readonly ITokenProvider _token = token;

    public async Task<User> User()
    {
        var token = _token.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext.Users
                                .AsNoTracking()
                                .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
    }
}
