using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _421final.Data.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamStyle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    style = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStyle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamStyleId = table.Column<int>(type: "int", nullable: false),
                    teamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyTeam_TeamStyle_TeamStyleId",
                        column: x => x.TeamStyleId,
                        principalTable: "TeamStyle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyTeam_TeamStyleId",
                table: "MyTeam",
                column: "TeamStyleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyTeam");

            migrationBuilder.DropTable(
                name: "TeamStyle");
        }
    }
}
