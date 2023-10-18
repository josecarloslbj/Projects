namespace JC.Core.Dtos
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        public string? Icon { get; set; }
        public int? Ordem { get; set; }
    }
}
