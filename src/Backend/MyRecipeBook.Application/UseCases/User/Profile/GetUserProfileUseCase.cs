﻿using AutoMapper;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Services;

namespace MyRecipeBook.Application;

public class GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper) : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.User();
        return _mapper.Map<ResponseUserProfileJson>(user);
    }
}
