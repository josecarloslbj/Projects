using FluentMigrator;

namespace JC.MySQLMigration.Migrations
{

    [Migration(3)]
    public class M0003_Localizacao : Migration
    {

        public override void Up()
        {
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/Pais.sql");
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/Regioes.sql");
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/Estados.sql");
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/Cidades.sql");

            string sql = @"UPDATE Pais set Padrao = 1 WHERE Sigla = 'BR'; ";
            sql += @"UPDATE Estado set Padrao = 1 WHERE Uf = 'MG'; ";
            sql += @"UPDATE Cidade set Padrao = 1 WHERE Nome = 'Belo Horizonte'; ";

            Execute.Sql(sql);
        }

        public override void Down()
        {

        }
    }
}
