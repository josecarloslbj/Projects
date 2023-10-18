using FluentMigrator;

namespace JC.MySQLMigration.Migrations
{
    [Migration(4)]
    public class M0004_Cadastros : Migration
    {

        public override void Up()
        {
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/GrupoProduto.sql");
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/Fabricante.sql");
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/Fornecedores.sql");
            Execute.Script("../JC.MySQLMigration/Scripts/Inserts/Produtos.sql");
        }

        public override void Down()
        {

        }
    }
}