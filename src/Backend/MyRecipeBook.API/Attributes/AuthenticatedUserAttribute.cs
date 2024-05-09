using MyRecipeBook.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MyRecipeBook.API.Attributes;

public class AuthenticatedUserAttribute: TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
