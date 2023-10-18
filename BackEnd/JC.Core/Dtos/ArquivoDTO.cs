namespace JC.Core.Dtos
{
    public class ArquivoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Diretorio { get; set; }
        public string? SubDiretorio { get; set; }
        public string? Extensao { get; set; }
        public string? Entidade { get; set; }
        public int? IdEntidade { get; set; }

        public string? UrlArquivo
        {
            get
            {
                return $"{SubDiretorio}/{Nome}";

            }
        }
    }
}
