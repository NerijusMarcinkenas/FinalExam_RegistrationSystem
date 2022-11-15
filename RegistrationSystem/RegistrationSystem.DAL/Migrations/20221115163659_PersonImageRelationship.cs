using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.DAL.Migrations
{
    public partial class PersonImageRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PersonImages_ImageId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_ImageId",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "PersonImages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_PersonImages_PersonId",
                table: "PersonImages",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonImages_People_PersonId",
                table: "PersonImages",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonImages_People_PersonId",
                table: "PersonImages");

            migrationBuilder.DropIndex(
                name: "IX_PersonImages_PersonId",
                table: "PersonImages");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonImages");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "People",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_People_ImageId",
                table: "People",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PersonImages_ImageId",
                table: "People",
                column: "ImageId",
                principalTable: "PersonImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
