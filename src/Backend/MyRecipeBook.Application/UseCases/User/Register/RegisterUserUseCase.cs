using MyRecipeBook.Application.Services;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases;

public class RegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnly;
    private readonly IUserWriteOnlyRepository _userWriteOnly;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        Validate(request);

        var user = autoMapper.Map<Domain.User>(request);
        user.Password = Encripter.Encrypt(request.Password);

        await _userWriteOnly.Add(user);

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
