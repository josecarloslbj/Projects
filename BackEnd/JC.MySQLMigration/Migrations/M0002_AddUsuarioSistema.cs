using FluentMigrator;
using JC.Infrastructure.Hash;

namespace JC.MySQLMigration.Migrations
{
    [Migration(2)]
    public class M0002_AddUsuarioSistema : Migration
    {
        public override void Up()
        {
            Execute.Script("../JC.MySQLMigration/Scripts/0002_InsertsDataBase.sql");

            DateTime dataCriacao = DateTime.Now;
            string dataCriacaoStr = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss");

            string senha = "sistema-123";
            string senhaGerada = Hash.HashSHA256(senha, dataCriacao.ToString(JC.Core.Comuns.Constantes.FORMAT_SALT));
            string md5SenhaGerada = Hash.EncryptMD5(senha);

            string sql = $@"INSERT INTO `Usuario` (`Nome`, `CPF`, `Email`,`Login`, `Senha`, `UltimaSenhaGerada`, `DataCadastro`, `Ativo`) 
VALUES ('Sistema', '01234567890', 'sistema@sistema.com', 'admin', '{senhaGerada}' , '{md5SenhaGerada}' , '{dataCriacaoStr}', '1');";

            senha = "jc-123";
            senhaGerada = Hash.HashSHA256(senha, dataCriacao.ToString(JC.Core.Comuns.Constantes.FORMAT_SALT));
            md5SenhaGerada = Hash.EncryptMD5(senha);
            sql += $@"INSERT INTO `Usuario` (`Nome`, `CPF`, `Email`,`Login`, `Senha`, `UltimaSenhaGerada`, `DataCadastro`, `Ativo`) 
VALUES ('José Carlos', '06260843690', 'josecarloslbj@gmail.com', 'josecarloslbj', '{senhaGerada}' , '{md5SenhaGerada}' , '{dataCriacaoStr}', '1');";

            senha = "5562";
            senhaGerada = Hash.HashSHA256(senha, dataCriacao.ToString(JC.Core.Comuns.Constantes.FORMAT_SALT));
            md5SenhaGerada = Hash.EncryptMD5(senha);
            sql += $@"INSERT INTO `Usuario` (`Nome`, `CPF`, `Email`,`Login`, `Senha`, `UltimaSenhaGerada`, `DataCadastro`, `Ativo`) 
VALUES ('Chirley Oliveira', '06260843690', 'chirley@gmail.com', 'chirley', '{senhaGerada}' , '{md5SenhaGerada}' , '{dataCriacaoStr}', '1');";

            string insertPerfil = $@"INSERT INTO `Perfil` (`Nome`, `Descricao`, `DataCriacao`, `Ativo`) VALUES ('Sistema', 'Perfil de Sistema', NOW(), '1');
                                     INSERT INTO `Perfil` (`Nome`, `Descricao`, `DataCriacao`, `Ativo`) VALUES ('Administrador', 'Administrador do Sistema', NOW(), '1');
                                     INSERT INTO `Perfil` (`Nome`, `Descricao`, `DataCriacao`, `Ativo`) VALUES ('Colaborador', 'Colaboradores da empresa', NOW(), '1');";

            string insertPerfilPermissao = $@"INSERT INTO `permissaoPerfil` (`Perfil_Id`, `Permissao_Id`) SELECT (SELECT id FROM perfil where nome  = 'admin' LIMIT 1) as Perfil_Id , id as Permissao_Id FROM Permissao; 
                                              INSERT INTO `permissaoPerfil` (`Perfil_Id`, `Permissao_Id`) SELECT (SELECT id FROM perfil where nome  = 'Administrador' LIMIT 1) as Perfil_Id , id as Permissao_Id FROM Permissao;  ";

            string insertPerfilUsuario = $@"INSERT INTO `PerfilUsuario` (`Perfil_Id`, `Usuario_Id`) SELECT id as Perfil_Id , (SELECT id FROM Usuario where login  = 'Sistema' LIMIT 1) as Usuario_Id  FROM storeShop.Perfil; 
                                      INSERT INTO `PerfilUsuario` (`Perfil_Id`, `Usuario_Id`) SELECT id as Perfil_Id , (SELECT id FROM Usuario where login  = 'josecarloslbj' LIMIT 1) as Usuario_Id  FROM storeShop.Perfil;
                                      INSERT INTO `PerfilUsuario` (`Perfil_Id`, `Usuario_Id`) SELECT id as Perfil_Id , (SELECT id FROM Usuario where login  = 'chirley' LIMIT 1) as Usuario_Id  FROM storeShop.Perfil;";

            Execute.Sql(sql, "Criação Usuário");
            Execute.Sql(insertPerfil, "Criação Perfil");
            Execute.Sql(insertPerfilPermissao, "Criação PermissaoPerfil");
            Execute.Sql(insertPerfilUsuario, "Criação PerfilUsuario");

        }

        public override void Down()
        {

        }
    }
}