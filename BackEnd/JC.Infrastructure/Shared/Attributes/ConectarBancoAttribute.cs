namespace JC.Infrastructure.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class ConectarBancoAttribute : Attribute
{
    public bool Conectar { get; set; }
    public bool IniciarTransacao { get; set; }

    public ConectarBancoAttribute(bool conectar = true, bool iniciarTransacao = true)
    {
        Conectar = conectar;
        IniciarTransacao = conectar && iniciarTransacao;
    }
}
