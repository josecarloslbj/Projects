using JC.Core.Entities.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.Core.Dtos
{
    public class EstadoDTO
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Sigla { get; set; }
        public string? Codigo { get; set; }
        public int IdPais { get; set; }
        public bool Padrao { get; set; }

        public virtual Pais? Pais { get; set; }
        public int? IdUsuario { get; set; }
    }
}
