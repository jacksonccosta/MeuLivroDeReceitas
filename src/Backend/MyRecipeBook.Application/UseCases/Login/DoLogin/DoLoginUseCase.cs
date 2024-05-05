using MyRecipeBook.Application.Services;
using MyRecipeBook.Communication;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application;

public class DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, Encripter encripter) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly Encripter _encripter = encripter;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encriptedPAssowrd = _encripter.Encrypt(request.Password);
        var user = await _userReadOnlyRepository.GetByEmailAndPassword(request.Email, encriptedPAssowrd) ?? throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
    }
}
