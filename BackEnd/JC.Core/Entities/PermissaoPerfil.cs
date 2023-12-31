﻿using JC.Core.Base;

namespace JC.Core.Entities
{
    public class PermissaoPerfil : EntidadeBase
    {
        public int IdPermissao { get; set; }
        public int IdPerfil { get; set; }

        public virtual Permissao Permissao { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}
