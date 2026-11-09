using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_25_EF_Core.Migrations
{
    /// <inheritdoc />
    public partial class EventOrganizerProfileTablesUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizerProfile",
                table: "OrganizerProfile");

            migrationBuilder.EnsureSchema(
                name: "OrganizerUser");

            migrationBuilder.RenameTable(
                name: "OrganizerProfile",
                newName: "OrganizerProfileTable",
                newSchema: "OrganizerUser");

            migrationBuilder.RenameColumn(
                name: "Biography",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                newName: "OrganizerDescription");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "100, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerDescription",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizerProfileTable",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                column: "OrganizerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizerProfileTable",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable");

            migrationBuilder.RenameTable(
                name: "OrganizerProfileTable",
                schema: "OrganizerUser",
                newName: "OrganizerProfile");

            migrationBuilder.RenameColumn(
                name: "OrganizerDescription",
                table: "OrganizerProfile",
                newName: "Biography");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerId",
                table: "OrganizerProfile",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "100, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "OrganizerProfile",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizerProfile",
                table: "OrganizerProfile",
                column: "OrganizerId");
        }
    }
}
