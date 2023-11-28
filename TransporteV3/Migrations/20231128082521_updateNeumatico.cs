using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransporteV3.Migrations
{
    public partial class updateNeumatico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Neumatico",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<int>(
                name: "Modelo",
                table: "Neumatico",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

        }
    }
}
