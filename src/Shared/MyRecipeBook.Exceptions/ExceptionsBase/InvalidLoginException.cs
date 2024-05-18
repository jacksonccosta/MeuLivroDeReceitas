using MyRecipeBook.Exceptions.ResourcesMessages;

namespace MyRecipeBook.Exceptions;

public class InvalidLoginException() : MyRecipeBookException(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
{

}
