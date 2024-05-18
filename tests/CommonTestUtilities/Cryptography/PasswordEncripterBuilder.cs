using MyRecipeBook.Infrastructure.Security;

namespace CommonTestUtilities;

public class PasswordEncripterBuilder
{
    public static Sha512Encript Build() => new("9Y[bWX<11ua}");
}
