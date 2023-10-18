namespace JC.Core.Dtos;

public class PaisDTO
{
    public int? Id { get; set; }
    public string? Nome { get; set; }
    public string? Codigo { get; set; }
    public string? Sigla { get; set; }
    public string? Descricao { get; set; }
    public string? Abreviacao { get; set; }
    public int? CodigoTelefone { get; set; }
    public bool Padrao { get; set; }
    public bool Ativo { get; set; }
}
