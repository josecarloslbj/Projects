using JC.Application;
using JC.Application.Services;
using JC.Core.Dtos;
using JC.Infrastructure.Shared.JwtUtils;
using Microsoft.Extensions.Options;

namespace JC.WebApi.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUsuarioService userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token!);
        if (userId != null)
        {
            UsuarioDTO usuarioAtual = await userService.ObterUsuario(userId.Value);
            usuarioAtual.Token = token;
            context.Items["User"] = usuarioAtual;

            //if (Contexto.Atual != null)
            //{
            //    // attach user to context on successful jwt validation
            //    UsuarioDTO usuarioAtual = await userService.ObterUsuario(userId.Value);
            //    usuarioAtual.Token = token;
            //    context.Items["User"] = usuarioAtual;
            //}
        }

        await _next(context);
    }
}