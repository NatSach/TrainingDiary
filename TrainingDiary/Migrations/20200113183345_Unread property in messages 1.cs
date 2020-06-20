using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingDiary.Migrations
{
    public partial class Unreadpropertyinmessages1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Message",
                newName: "IsReadBySender");

            migrationBuilder.AddColumn<bool>(
                name: "IsReadByReceiver",
                table: "Message",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReadByReceiver",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "IsReadBySender",
                table: "Message",
                newName: "IsRead");
        }
    }
}
