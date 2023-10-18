using JC.Core.Base;

namespace JC.Core.Entities
{
    public class Produto : EntidadeBase
    {
        public string Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdFabricante { get; set; }
        public int? IdFornecedor { get; set; }
        public string? Tipo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal PercentIPI { get; set; }
        public decimal PercentLucro { get; set; }
        public int Qtde { get; set; }
        public int QtdeMin { get; set; }
        public string? Localizacao { get; set; }
        public string? UnidadeMedida { get; set; }
        public int? IdArquivo { get; set; }
        public virtual Arquivo? Arquivo { get; set; }
        public int? IdUsuario { get; set; }
    }
}
