﻿namespace JC.Core.Dtos
{
    public class FornecedorDTO
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? RazaoSocial { get; set; }
        public int TipoPessoa { get; set; }
        public string? CPF_CNPJ { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? InscricaoMunicipal { get; set; }
        public string? WebSite { get; set; }
        public string? Email { get; set; }
        public string? Observacao { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public int? IdEndereco { get; set; }
        public int? IdContato { get; set; }
    }
}
