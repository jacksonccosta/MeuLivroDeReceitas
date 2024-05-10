using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application;

public interface IGetUserProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}
