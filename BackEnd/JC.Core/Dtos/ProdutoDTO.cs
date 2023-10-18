namespace JC.Core.Dtos
{
    public class ProdutoDTO
    {
        public int? Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        public int? IdGrupo { get; set; }

        public int? IdFabricante { get; set; }

        public int? IdFornecedor { get; set; }
        public string? Tipo { get; set; }

        public int? IdUsuario { get; set; }

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
        public string? UrlArquivo
        {
            get
            {
                if (this.IdArquivo == null)
                    return "Comuns/Images/produto-sem-img.png";

                return $"Comuns/Images/{Arquivo?.SubDiretorio}/{Arquivo?.Nome}";
            }
        }
        public virtual ArquivoDTO? Arquivo { get; set; }
        public bool Ativo { get; set; }

        public string? Situacao { get; set; }

    }
}
