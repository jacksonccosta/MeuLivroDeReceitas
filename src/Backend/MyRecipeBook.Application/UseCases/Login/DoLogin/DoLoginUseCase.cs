using MyRecipeBook.Communication;
using MyRecipeBook.Domain;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application;

public class DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, 
                            IPasswordEncripter encripter, 
                            IAccessTokenGenerator accessTokenGenerator) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IPasswordEncripter _encripter = encripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encriptedPAssowrd = _encripter.Encrypt(request.Password);
        var user = await _userReadOnlyRepository.GetByEmailAndPassword(request.Email, encriptedPAssowrd) ?? throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokenJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
