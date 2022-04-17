using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _421final.Data.Migrations
{
    public partial class updatingplayerapiclassnullable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRoot_AllPlayersTeamDef_TeamId",
                table: "PlayerRoot");

            migrationBuilder.DropTable(
                name: "AllPlayersTeamDef");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRoot_TeamRoot_TeamId",
                table: "PlayerRoot",
                column: "TeamId",
                principalTable: "TeamRoot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRoot_TeamRoot_TeamId",
                table: "PlayerRoot");

            migrationBuilder.CreateTable(
                name: "AllPlayersTeamDef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Championships = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllPlayersTeamDef", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRoot_AllPlayersTeamDef_TeamId",
                table: "PlayerRoot",
                column: "TeamId",
                principalTable: "AllPlayersTeamDef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
