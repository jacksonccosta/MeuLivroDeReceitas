using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Communication;
using MyRecipeBook.Exceptions.ResourcesMessages;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebAPI.Test.Login;

public class DoLoginTest : MyRecipeBookClassFixture
{
    private readonly string method = "login";

    private readonly string _name;
    private readonly string _email;
    private readonly string _password;

    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _name = factory.GetName();
        _email = factory.GetEmail();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };

        var response = await DoPost(method, request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_name);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Login_Invalid(string culture)
    {
        var request = RequestLoginJsonBuilder.Build();        

        var response = await DoPost(method, request, culture);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));
        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
