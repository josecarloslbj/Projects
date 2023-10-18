using JC.Core.Entities.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.Core.Dtos
{
    public class CidadeDTO
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Codigo { get; set; }
        public int IdEstado { get; set; }
        public bool Padrao { get; set; }
        public virtual Estado? Estado { get; set; }
        public int? IdUsuario { get; set; }
    }
}
