﻿namespace JC.Infrastructure.Shared.JwtUtils;

public class AppSettings
{
    public string Secret { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }

    // refresh token time to live (in days), inactive tokens are
    // automatically deleted from the database after this time
    public int RefreshTokenTTL { get; set; }
}
