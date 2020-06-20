using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingDiary.Migrations
{
    public partial class UpdateRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "Message",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "Message",
                nullable: true);
        }
    }
}
