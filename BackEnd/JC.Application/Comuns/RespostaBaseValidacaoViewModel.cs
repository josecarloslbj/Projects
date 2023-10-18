using Newtonsoft.Json;

namespace JC.Application.Comuns;

public class RespostaBaseViewModel
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? Id { get; set; }
    public string MensagemRetorno { get; set; }
    public string Excecao { get; set; }
    private int _status;
    public int Status { get { return _status; } set { _status = value; Sucesso = _status == 0; } }
    public List<RespostaBaseValidacaoViewModel> Erros { get; set; }
    public RespostaBaseViewModel()
    {
        Sucesso = true;
    }
    public RespostaBaseViewModel(string msg, int status)
    {
        MensagemRetorno = msg;
        Status = status;
    }

    /// <summary>
    /// Assinatura específica para repostas AJAX. É utilizada no cliente para identificar se é uma resposta tratada pela API
    /// </summary>
    public bool __isAPI { get; } = true;

    public bool Sucesso { get; set; }
    public string TargetUrl { get; set; }

    public string ToJsonString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class RespostaBaseValidacaoViewModel
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Campo { get; }

    public string Mensagem { get; }

    public RespostaBaseValidacaoViewModel(string campo, string mensagem)
    {
        Campo = campo != string.Empty ? campo : null;
        Mensagem = mensagem;
    }
}

public class RespostaBaseViewModel<T> : RespostaBaseViewModel
{
    public RespostaBaseViewModel()
    { }

    public RespostaBaseViewModel(T obj)
    {
        Conteudo = obj;
    }
    public T Conteudo { get; set; }
}

public class RespostaBaseViewModelGenerico : RespostaBaseViewModel
{
    public RespostaBaseViewModelGenerico()
    { }

    public object Conteudo { get; set; }
}

public class RespostaBasePaginadoViewModelGenerico : RespostaBaseViewModelGenerico
{
    public RespostaBasePaginadoViewModelGenerico()
    { }

    public int TotalItens { get; set; }
    public int TotalPaginas { get; set; }
    public int PaginaAtual { get; set; }
}