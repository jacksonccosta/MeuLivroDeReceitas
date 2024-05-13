using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application;

public interface IUpdateUseCase
{
    public Task Execute(RequestUpdateUserJson request);
}
