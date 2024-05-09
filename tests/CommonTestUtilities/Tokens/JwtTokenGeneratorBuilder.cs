using MyRecipeBook.Domain;
using MyRecipeBook.Infrastructure;

namespace CommonTestUtilities;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "112#YC12ZVNew]PAW{CT{u17u259j>8Av)");
}
