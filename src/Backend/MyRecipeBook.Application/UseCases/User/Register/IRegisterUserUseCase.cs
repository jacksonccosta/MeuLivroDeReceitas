using MyRecipeBook.Communication;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
