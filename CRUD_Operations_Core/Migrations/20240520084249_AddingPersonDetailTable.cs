using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_Operations_Core.Migrations
{
    public partial class AddingPersonDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    PhotoUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    cId = table.Column<int>(type: "int", nullable: false),
                    cName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cnId = table.Column<int>(type: "int", nullable: false),
                    cnName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonDetails");
        }
    }
}
