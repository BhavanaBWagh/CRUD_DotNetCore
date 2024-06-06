using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_Operations_Core.Migrations
{
    public partial class UpdatingDatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RowNumber",
                table: "PersonDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RowNumber",
                table: "PersonDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
