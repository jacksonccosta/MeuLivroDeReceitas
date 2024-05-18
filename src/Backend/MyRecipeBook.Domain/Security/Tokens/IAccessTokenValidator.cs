namespace MyRecipeBook.Domain;

public interface IAccessTokenValidator
{
    public Guid ValidateAndGetUserIdentifier(string token);
}
