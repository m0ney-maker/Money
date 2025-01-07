﻿using Microsoft.AspNetCore.Http;
using Money.BL.Interfaces;
using Money.Common;

namespace Money.BL.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var userId = Guid.Parse(httpContext.User.FindFirst(CustomClaimTypes.UserId).Value);
        return userId;
    }
}
