using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class ThirdDesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Patrons",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Patrons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
