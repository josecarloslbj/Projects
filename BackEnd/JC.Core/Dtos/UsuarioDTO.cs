using JC.Core.Entities;

namespace JC.Core.Dtos
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public string? Token { get; set; }
        public string? UltimaSenhaGerada { get; set; }
        public DateTime? DataCadastro { get; set; }
        public virtual IList<PerfilDTO>? Perfis { get; set; }
        public int? IdArquivo { get; set; }
        public virtual Arquivo? Arquivo { get; set; }

        public int? IdEndereco { get; set; }
        public virtual Endereco? Endereco { get; set; }
        public string RefreshToken { set; get; }
        public DateTime? RefreshTokenExpiryTime { set; get; }
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Nome)) return false;
            if (string.IsNullOrEmpty(Login)) return false;
            if (string.IsNullOrEmpty(Senha)) return false;

            return true;
        }
    }
}
