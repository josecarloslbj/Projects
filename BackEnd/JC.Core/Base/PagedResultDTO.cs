

namespace JC.Core.Base;

public class PagedResultDTO<TEntity>
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }

    public IList<TEntity> Items
    {
        get { return _items ?? (_items = new List<TEntity>()); }
        set { _items = value; }
    }
    private IList<TEntity> _items;

    public PagedResultDTO() { }

    public PagedResultDTO(IList<TEntity> items)
    {
        Items = items;
    }
}
