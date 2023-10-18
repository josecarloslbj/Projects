namespace JC.Core.Dtos
{
    public class PerfilDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataCriacao { get; set; }

        public List<PermissaoDTO>? Permissoes { get; set; }
    }
}
