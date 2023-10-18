namespace JC.Application
{
    public static class Contexto
    {
        public static ContextoAtual? Atual { get; set; }
        public static string? FusoHorario { get { return Atual?.FusoHorario; } }
        public static string? Token { get { return Atual?.Token; } }
        public static bool ConectarBanco => Atual != null && Atual.ConectarBanco;
        public static bool IniciarTransacao => Atual != null && Atual.IniciarTransacao;

        //public static DbSession Session { get { return Atual?.DbSession!; } }
        //public static IDbConnection Connection { get { return Atual?.Connection!; } }
        //public static IDbTransaction Transaction { get { return Atual?.Transaction!; } }

        public static void Salvar(ContextoAtual dto)
        {
            Contexto.Atual = dto;
        }
    }
}
