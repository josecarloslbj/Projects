using JC.Core.Comuns;

namespace JC.Application
{
    public class ContextoAtual
    {
        public ContextoAtual()
        {
            Guid = Guid.NewGuid();
            SistemaOperacional = SistemaOperacional.NAO_IDENTIFICADO;
        }
        public Guid Guid { get; set; }

        public int Id { get; set; }
        public int IdUsuario { get { return Id; } }
        public string? Contexto { get; set; }
        public bool UserIsAdmin => Login == Constantes.LOGIN_USUARIO_ADMIN;
        public bool UserIsSistema => Login == Constantes.LOGIN_USUARIO_SISTEMA;
        public string Login { get; set; }
        public string Token { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Idioma Idioma { get; set; }

        public string ConnectionString { get; set; }
        public string FusoHorario { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public SistemaOperacional SistemaOperacional { get; set; }
        public bool ConectarBanco { get; set; }
        public bool IniciarTransacao { get; set; }

        //public DbSession DbSession { get; set; }
        //public IDbConnection Connection { get; set; }
        //public IDbTransaction Transaction { get; set; }
        public string IdiomaSigla
        {
            get
            {
                switch (Idioma)
                {
                    case Idioma.es_MX:
                        return "es-MX";
                    case Idioma.en_US:
                        return "en-US";
                    default:
                        return "pt-BR";
                }
            }
        }
    }
}
