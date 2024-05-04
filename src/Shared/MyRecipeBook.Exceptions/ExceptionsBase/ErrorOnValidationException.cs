namespace MyRecipeBook.Exceptions;

public class ErrorOnValidationException(IList<string> errorMessages) : MyRecipeBookException(string.Empty)
{
    public IList<string> ErrorMessages { get; set; } = errorMessages;
}
