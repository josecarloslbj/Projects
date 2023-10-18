
using JC.Core.Base;

namespace JC.Core.Entities;

public class Categoria : EntidadeBase
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Icon { get; set; }
    public int? Ordem { get; set; }
    public virtual IList<Permissao>? Permissoes { get; set; }

    public int? IdUsuario { get; set; }

    //public List<HistoricoCaixa>? HistoricoCaixa { get; set; }

    //public void RealizarEntrada(HistoricoCaixa hist)
    //{
    //    HistoricoCaixa?.Add(hist);
    //}

    //public void RealizarSaida(HistoricoCaixa hist)
    //{
    //    HistoricoCaixa?.Add(hist);
    //}
}
