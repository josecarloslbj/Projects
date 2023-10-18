using JC.Core.Entities.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.Core.Dtos
{
    public class BairroDTO
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? ZonaGeografica { get; set; }
        public string? RegionalAdministrativa { get; set; }
        public int Populacao { get; set; }
        public decimal AreaKm2 { get; set; }
        public int IdCidade { get; set; }
        public int? IdUsuario { get; set; }
        public bool Padrao { get; set; }
        public bool Ativo { get; set; }

        public CidadeDTO Cidade { get; set; }
    }
}
