using FluentMigrator;

namespace JC.MySQLMigration.Migrations
{
    [Migration(1)]
    public class M0001_StartDataBase : Migration
    {
        public override void Up()
        {
            Execute.Script("../JC.MySQLMigration/Scripts/0001_StartDataBase.sql");
        }

        public override void Down()
        {

        }
    }
}
