using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_25_EF_Core.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipbetweenAttendeesandBadgetablesOnetoOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "AttendeeId",
                table: "Badge",
                newName: "BadgeAttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Badge_BadgeAttendeeId",
                table: "Badge",
                column: "BadgeAttendeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Badge_Attendee_BadgeAttendeeId",
                table: "Badge",
                column: "BadgeAttendeeId",
                principalTable: "Attendee",
                principalColumn: "EventAttendeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badge_Attendee_BadgeAttendeeId",
                table: "Badge");

            migrationBuilder.DropIndex(
                name: "IX_Badge_BadgeAttendeeId",
                table: "Badge");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                schema: "OrganizerUser",
                table: "OrganizerProfileTable",
                newName: "OrganizerId");

            migrationBuilder.RenameColumn(
                name: "BadgeAttendeeId",
                table: "Badge",
                newName: "AttendeeId");
        }
    }
}
