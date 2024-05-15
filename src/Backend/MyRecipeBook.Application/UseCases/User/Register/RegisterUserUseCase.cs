using AutoMapper;
using MyRecipeBook.Communication;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ResourcesMessages;

namespace MyRecipeBook.Application.UseCases;

public class RegisterUserUseCase(IUserReadOnlyRepository userReadOnly, 
                                IUserWriteOnlyRepository userWriteOnly, 
                                IMapper mapper, IUnitOfWork unitOfWork,
                                IPassqordEncript passwordEncrypter,
                                IAccessTokenGenerator accessTokenGenerator) : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnly = userReadOnly;
    private readonly IUserWriteOnlyRepository _userWriteOnly = userWriteOnly;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPassqordEncript _passwordEncrypter = passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnly.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            Tokens = new ResponseTokenJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate (RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailExist = await _userReadOnly.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTRRED));

        if (!result.IsValid)
        {
            var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(erroMessages);
        }
    }
}
