using CommonTestUtilities;
using FluentAssertions;
using MyRecipeBook.Application.UseCases;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ResourcesMessages;

namespace UseCases.Test;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
                
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Tokens.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTRRED));
    }

    [Fact]
    public async Task Error_Name_Empyt()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Email_Empyt()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.EMAIL_EMPYT));
    }
    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var useReadOnlyBuilder = new UserReadOnlyRepositoryBuilder();
        var userWriteOnly = UserWriteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (!string.IsNullOrEmpty(email))
            useReadOnlyBuilder.ExistActiveUserWithEmail(email);

        return new RegisterUserUseCase(useReadOnlyBuilder.Build(), userWriteOnly, mapper, unitOfWork, passwordEncripter, accessTokenGenerator);
    }
}
