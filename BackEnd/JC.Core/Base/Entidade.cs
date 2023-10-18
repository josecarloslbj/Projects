namespace JC.Core.Base;

public abstract class Entidade<TKey>
{
    public Entidade()
    {
        this.Ativo = true;
    }

    public virtual TKey? Id { get; set; }

    public bool Ativo { get; set; }

    public bool IsEdicao
    {
        get
        {
            if (Id != null)
            {
                if (Id.GetType() == typeof(int))
                    return Convert.ToInt32(Id) > 0;

                if (Id.GetType() == typeof(ulong))
                    return Convert.ToUInt64(Id) > 0;

                if (Id.GetType() == typeof(long))
                    return Convert.ToInt64(Id) > 0;

                if (Id.GetType() == typeof(string))
                    return !string.IsNullOrEmpty(Id.ToString());

                if (Id.GetType() == typeof(Guid))
                    return !string.IsNullOrEmpty(Id.ToString());
            }

            return true;
        }
    }
}
