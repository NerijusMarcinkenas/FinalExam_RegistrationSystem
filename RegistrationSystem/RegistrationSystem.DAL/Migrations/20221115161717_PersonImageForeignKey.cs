using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.DAL.Migrations
{
    public partial class PersonImageForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "People",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PersonImages_ImageId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_ImageId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "PersonImages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
    }
}
