using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingDiary.Migrations
{
    public partial class CampModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampId",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Start_Location",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEvent_CampId",
                table: "BaseEvent",
                column: "CampId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEvent_BaseEvent_CampId",
                table: "BaseEvent",
                column: "CampId",
                principalTable: "BaseEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEvent_BaseEvent_CampId",
                table: "BaseEvent");

            migrationBuilder.DropIndex(
                name: "IX_BaseEvent_CampId",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "CampId",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "Start_Location",
                table: "BaseEvent");
        }
    }
}
