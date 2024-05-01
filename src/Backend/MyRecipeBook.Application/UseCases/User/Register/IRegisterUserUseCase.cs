﻿using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}