using JC.Infrastructure.Shared;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;


namespace JC.WebApi.Middlewares
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : Attribute
    {
    }

    public class DbTransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public DbTransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, DbSession connectionProvider)
        {
            // For HTTP GET opening transaction is not required
            if (httpContext.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
            {
                await _next(httpContext);
                return;
            }

            // If action is not decorated with TransactionAttribute then skip opening transaction
            var endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<TransactionAttribute>();


            var attributeConectarBancoAttribute = endpoint?.Metadata.GetMetadata<ConectarBancoAttribute>();


            if (attributeConectarBancoAttribute == null)
            {
                await _next(httpContext);
                return;
            }

            IDbTransaction? transaction = null;

            try
            {
                if (!attributeConectarBancoAttribute.Conectar)
                {
                    await _next(httpContext);
                    return;
                }

                if (attributeConectarBancoAttribute.IniciarTransacao)
                    transaction = connectionProvider.CreateTransaction();

                await _next(httpContext);


                if (transaction != null)
                    transaction.Commit();
            }
            catch (Exception error)
            {
                //if (transaction != null)
                //    transaction.Rollback();

                //var response = httpContext.Response;
                //response.ContentType = "application/json";

                //RespostaBaseViewModelGenerico resposta = new RespostaBaseViewModelGenerico();

                //var statusCode = System.Net.HttpStatusCode.OK;
                //var exception = error;
                //if (exception != null)
                //{
                //    var exValidate = exception;
                //    while (exValidate != null && !(exValidate is DomainLayerException))
                //    {
                //        if (exValidate.InnerException is DomainLayerException)
                //        {
                //            exception = exValidate.InnerException;
                //            break;
                //        }
                //        exValidate = exValidate.InnerException;
                //    }

                //    if (exception is DomainLayerException dle)
                //    {
                //        resposta.MensagemRetorno = dle.Message;

                //        if (dle.Erros != null && dle.Erros.Any())
                //            resposta.Erros = dle.Erros;

                //        resposta.Status = dle.Status;
                //        resposta.Excecao = dle.Message;
                //        statusCode = dle.HttpStatusCode;

                //        //logger.LogDebug(exception, exception.Message);
                //    }
                //    else if (exception is Exception)
                //    {
                //        resposta.MensagemRetorno = exception.Message;
                //        resposta.Excecao = exception.GetExceptionText();
                //        resposta.Status = -1;

                //    }
                //}

                //var result = new JsonResult(resposta);
                //result.StatusCode = (int)statusCode;
                //await response.WriteAsync(JsonSerializer.Serialize(result));
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app)
            => app.UseMiddleware<DbTransactionMiddleware>();
    }
}
