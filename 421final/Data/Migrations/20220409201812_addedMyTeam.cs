using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _421final.Data.Migrations
{
    public partial class addedMyTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGId = table.Column<int>(type: "int", nullable: true),
                    SGId = table.Column<int>(type: "int", nullable: true),
                    SFId = table.Column<int>(type: "int", nullable: true),
                    PFId = table.Column<int>(type: "int", nullable: true),
                    CId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyTeam_Player_CId",
                        column: x => x.CId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyTeam_Player_PFId",
                        column: x => x.PFId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyTeam_Player_PGId",
                        column: x => x.PGId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyTeam_Player_SFId",
                        column: x => x.SFId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyTeam_Player_SGId",
                        column: x => x.SGId,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyTeam_CId",
                table: "MyTeam",
                column: "CId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTeam_PFId",
                table: "MyTeam",
                column: "PFId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTeam_PGId",
                table: "MyTeam",
                column: "PGId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTeam_SFId",
                table: "MyTeam",
                column: "SFId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTeam_SGId",
                table: "MyTeam",
                column: "SGId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyTeam");
        }
    }
}
