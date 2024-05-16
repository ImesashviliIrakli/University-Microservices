﻿using Microsoft.AspNetCore.Http;
using University.Shared.Interfaces.AuthInterfaces;
using University.Shared.Utility;

namespace University.Shared.Services.AuthServices;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    public TokenProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void ClearToken()
    {
        _contextAccessor.HttpContext.Response.Cookies.Delete(SD.TokenCookie);
    }

    public string GetToken()
    {
        string token;

        bool hasToken = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.TokenCookie, out token);

        return hasToken ? token : null;
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext.Response.Cookies.Append(SD.TokenCookie, token);
    }
}
