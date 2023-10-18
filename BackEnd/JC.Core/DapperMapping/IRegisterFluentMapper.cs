using Dapper.FluentMap.Configuration;

namespace JC.Core.DapperMapping
{
    /// <summary>
    /// Interface usada para marcar os mapeadores do Dommel.
    /// O container IoC (StructureMap) irá carregar todas as implementações
    /// desta interface e irá registrar o mapeamento.
    /// </summary>
    public interface IRegisterFluentMapper
    {
        void RegisterMap(FluentMapConfiguration config);
    }
}
