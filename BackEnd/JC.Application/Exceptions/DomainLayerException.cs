using JC.Application.Comuns;
using System.Net;

namespace JC.Application.Exceptions;

public class DomainLayerException : Exception
{

    public DomainLayerException(string message, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message)
    {
        HttpStatusCode = statusCode;
        Status = status;
    }

    public DomainLayerException(string message, string details, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message)
    {
        HttpStatusCode = statusCode;
        Status = status;
        Details = details;
    }

    public DomainLayerException(string message, string details, string detailsInHtml, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message)
    {
        HttpStatusCode = statusCode;
        Status = status;
        Details = details;
        DetailsInHtml = detailsInHtml;
    }


    public DomainLayerException(string message, Exception innerException, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message, innerException)
    {
        HttpStatusCode = statusCode;
        Status = status;
    }

    public DomainLayerException(string message, List<RespostaBaseValidacaoViewModel> erros, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message)
    {
        HttpStatusCode = statusCode;
        Status = status;
        Erros = erros;
    }


    public int Status { get; protected set; }
    public HttpStatusCode HttpStatusCode { get; protected set; }
    public string Details { get; set; }
    public string DetailsInHtml { get; set; }

    public List<RespostaBaseValidacaoViewModel> Erros { get; set; }
}

