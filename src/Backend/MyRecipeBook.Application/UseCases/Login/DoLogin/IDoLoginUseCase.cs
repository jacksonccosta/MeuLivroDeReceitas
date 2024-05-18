using MyRecipeBook.Communication;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application;

public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
