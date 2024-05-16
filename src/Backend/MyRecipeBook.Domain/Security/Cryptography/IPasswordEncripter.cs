namespace MyRecipeBook.Domain;

public interface IPasswordEncripter
{
    string Encrypt(string str);
}
