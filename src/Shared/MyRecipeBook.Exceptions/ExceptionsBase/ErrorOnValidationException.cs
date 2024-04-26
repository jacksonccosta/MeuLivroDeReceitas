namespace MyRecipeBook.Exceptions;

public class ErrorOnValidationException(IList<string> erroMessages) : MyRecipeBookException
{
    public IList<string> ErrorMessages { get; set; } = erroMessages;
}
