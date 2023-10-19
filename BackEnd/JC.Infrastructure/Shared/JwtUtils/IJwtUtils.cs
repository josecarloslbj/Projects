﻿using JC.Infrastructure.Shared.Authorization.Model;

namespace JC.Infrastructure.Shared.JwtUtils;

public interface IJwtUtils
{
    public string GenerateJwtToken(User user);
    public int? ValidateJwtToken(string token);
    public RefreshToken GenerateRefreshToken(string ipAddress);
}
