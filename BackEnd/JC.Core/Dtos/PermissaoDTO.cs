namespace JC.Core.Dtos
{
    public class PermissaoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Ajuda { get; set; }
        public CategoriaDTO? Categoria { get; set; }

    }
}
