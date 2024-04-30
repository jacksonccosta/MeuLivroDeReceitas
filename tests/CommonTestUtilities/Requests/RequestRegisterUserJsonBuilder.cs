using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(int passwordLength = 7)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name,null,"outlook.com"))
            .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
    }
}
