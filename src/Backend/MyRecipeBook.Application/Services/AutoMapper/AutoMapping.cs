using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain;

namespace MyRecipeBook.Application.Services;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {
        CreateMap<User, ResponseUserProfileJson>();
    }
}
