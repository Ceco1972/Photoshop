using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoContest.DataBase.Migrations
{
    public partial class IssuewithPictureinsert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Votes_VoteId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_VoteId",
                table: "Pictures");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PictureId",
                table: "Votes",
                column: "PictureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Pictures_PictureId",
                table: "Votes",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Pictures_PictureId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_PictureId",
                table: "Votes");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_VoteId",
                table: "Pictures",
                column: "VoteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Votes_VoteId",
                table: "Pictures",
                column: "VoteId",
                principalTable: "Votes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
