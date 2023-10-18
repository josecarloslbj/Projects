using Dapper.FluentMap.Configuration;
using Dapper.FluentMap.Dommel.Mapping;
using JC.Core.Base;

namespace JC.Core.DapperMapping;

public abstract class BaseMap<TEntity> : DommelEntityMap<TEntity>, IRegisterFluentMapper where TEntity : EntidadeBase
{
    public BaseMap()
    {
        Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
        Map(x => x.IsEdicao).Ignore();
    }

    public virtual void RegisterMap(FluentMapConfiguration config)
    {
        config.AddMap(this);
    }
}
