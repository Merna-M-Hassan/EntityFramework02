using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_25_EF_Core.Migrations
{
    /// <inheritdoc />
    public partial class createaonetomanyrelationshipTotalFromMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badge_Attendee_BadgeAttendeeId",
                table: "Badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventOrganizer",
                table: "EventOrganizer");

            migrationBuilder.RenameTable(
                name: "EventOrganizer",
                newName: "TableOrganizer");

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableOrganizer",
                table: "TableOrganizer",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizerProfileTable_OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                column: "OrganizerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventsTable_OrganizerId",
                table: "EventsTable",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Badge_Attendee_BadgeAttendeeId",
                table: "Badge",
                column: "BadgeAttendeeId",
                principalTable: "Attendee",
                principalColumn: "EventAttendeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsTable_TableOrganizer_OrganizerId",
                table: "EventsTable",
                column: "OrganizerId",
                principalTable: "TableOrganizer",
                principalColumn: "OrganizerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizerProfileTable_TableOrganizer_OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                column: "OrganizerId",
                principalTable: "TableOrganizer",
                principalColumn: "OrganizerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badge_Attendee_BadgeAttendeeId",
                table: "Badge");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsTable_TableOrganizer_OrganizerId",
                table: "EventsTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizerProfileTable_TableOrganizer_OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable");

            migrationBuilder.DropIndex(
                name: "IX_OrganizerProfileTable_OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable");

            migrationBuilder.DropIndex(
                name: "IX_EventsTable_OrganizerId",
                table: "EventsTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableOrganizer",
                table: "TableOrganizer");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable");

            migrationBuilder.RenameTable(
                name: "TableOrganizer",
                newName: "EventOrganizer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventOrganizer",
                table: "EventOrganizer",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Badge_Attendee_BadgeAttendeeId",
                table: "Badge",
                column: "BadgeAttendeeId",
                principalTable: "Attendee",
                principalColumn: "EventAttendeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
