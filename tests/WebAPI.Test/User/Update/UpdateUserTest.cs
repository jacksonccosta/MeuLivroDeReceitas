using CommonTestUtilities;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using MyRecipeBook.Exceptions.ResourcesMessages;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebAPI.Test.User.Update;

public class UpdateUserTest : MyRecipeBookClassFixture
{
    private const string METHOD = "user";
    private readonly Guid _userIdentifier;

    public UpdateUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var response = await DoPut(METHOD, request, token);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empyt_Name(string culture)
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var response = await DoPut(METHOD, request, token, culture);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}
