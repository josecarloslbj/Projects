namespace JC.Infrastructure.Shared.Authorization.Model;

using System.Text.Json.Serialization;
public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string JwtToken { get; set; }
    public virtual List<string> Permissoes { get; set; }

    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; }

    public AuthenticateResponse(User user, string jwtToken, string refreshToken, List<string> permissoes)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
        Permissoes = permissoes;
    }
}