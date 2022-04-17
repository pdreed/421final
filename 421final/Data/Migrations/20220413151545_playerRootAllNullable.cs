using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _421final.Data.Migrations
{
    public partial class playerRootAllNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRoot_TeamRoot_TeamId",
                table: "PlayerRoot");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "PlayerRoot",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "PlayerRoot",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "PlayerRoot",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRoot_TeamRoot_TeamId",
                table: "PlayerRoot",
                column: "TeamId",
                principalTable: "TeamRoot",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRoot_TeamRoot_TeamId",
                table: "PlayerRoot");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "PlayerRoot",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "PlayerRoot",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "PlayerRoot",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRoot_TeamRoot_TeamId",
                table: "PlayerRoot",
                column: "TeamId",
                principalTable: "TeamRoot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
