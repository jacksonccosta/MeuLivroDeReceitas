namespace MyRecipeBook.Domain;

public interface IAccessTokenGenerator
{
    public string Generate(Guid userIdentifier);
}
