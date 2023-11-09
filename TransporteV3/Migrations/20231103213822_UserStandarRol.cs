using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransporteV3.Migrations
{
    public partial class UserStandarRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF NOT EXISTS(Select Id from AspNetRoles where Id = 'c8ab18be-2055-410c-b49f-1fb3948dea7f')
BEGIN
	INSERT AspNetRoles (Id, [Name], [NormalizedName])
	VALUES ('c8ab18be-2055-410c-b49f-1fb3948dea7f', 'userstandar', 'USERSTANDAR')
END

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles Where Id = 'c8ab18be-2055-410c-b49f-1fb3948dea7f'");
        }
    }
}
