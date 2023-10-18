using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.Core.Dtos
{
    public class EstoqueProdutoDTO
    {
        public int? Id { get; set; }
        public int? QtdeAtual { get; set; }
        public int? IdProduto { get; set; }
        public DateTime? DataReposicao { get; set; }
        public int? QtdeUltimaReposicao { get; set; }
        public int? IdUsuario { get; set; }
    }
}
