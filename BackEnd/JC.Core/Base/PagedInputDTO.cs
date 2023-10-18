using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.Core.Base;

public class PagedInputDTO
{

    public int? PageSize
    {
        get
        {
            if (!Rows.HasValue) return int.MaxValue;
            else
                return this.Rows;
        }

    }

    public int? Rows { get; set; }

    public int First { get; set; }
    public string? SortField { get; set; }
    public int SortOrder { get; set; }
    public int Row { get; set; }
    public bool? MultiSortMeta { get; set; }

    public string? GlobalFilter { get; set; }
    public Object? Filters { get; set; }

    public List<ParametrosFilters>? Filtros { get; set; }
}


public class ParametrosFilters
{
    public string? Condicao { get; set; }
}