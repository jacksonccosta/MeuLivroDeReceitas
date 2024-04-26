using MyRecipeBook.Application.Services;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
    {
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        Validate(request);

        var user = autoMapper.Map<Domain.User>(request);
        user.Password = Encripter.Encrypt(request.Password);

        //Salvar no banco de dados

        return new ResponseRegisteredUserJson()
        {
            Name = request.Name
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(erroMessages);
        }
    }
}
