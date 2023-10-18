using JC.Core.Base;

namespace JC.Core.Entities
{
    public class Arquivo : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Diretorio { get; set; }
        public string? SubDiretorio { get; set; }
        public string? Extensao { get; set; }
        public string? Entidade { get; set; }
        public int? IdEntidade { get; set; }
        public int? IdUsuario { get; set; }
    }
}
