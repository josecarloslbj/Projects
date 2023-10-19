namespace JC.Infrastructure.Shared.Authorization.Model;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public List<RefreshToken> RefreshTokens { get; set; }
}