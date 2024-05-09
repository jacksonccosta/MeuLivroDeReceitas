using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyRecipeBook.Infrastructure.Security;

public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string signgKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signgKey);
        return new SymmetricSecurityKey(bytes);
    }
}
