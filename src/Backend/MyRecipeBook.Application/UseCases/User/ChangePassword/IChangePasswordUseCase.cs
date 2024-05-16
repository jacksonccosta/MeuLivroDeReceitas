using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
