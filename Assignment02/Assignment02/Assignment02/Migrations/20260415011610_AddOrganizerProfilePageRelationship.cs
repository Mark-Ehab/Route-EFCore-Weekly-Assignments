using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment02.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizerProfilePageRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    ShortBiography = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LinkToPersonalSite = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    LinkToCompanySite = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePages", x => new { x.Id, x.OrganizerId });
                    table.ForeignKey(
                        name: "FK_ProfilePages_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePages_OrganizerId",
                table: "ProfilePages",
                column: "OrganizerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilePages");

            migrationBuilder.DropTable(
                name: "Organizers");
        }
    }
}
