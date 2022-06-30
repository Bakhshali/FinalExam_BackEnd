using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Exam_Back_End.Migrations
{
    public partial class addedNetworking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FbUrl",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstaUrl",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkypeUrl",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FbUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "InstaUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "SkypeUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Teams");
        }
    }
}
