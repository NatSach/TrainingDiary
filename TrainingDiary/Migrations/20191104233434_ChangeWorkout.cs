using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingDiary.Migrations
{
    public partial class ChangeWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullWorkout");

            migrationBuilder.DropColumn(
                name: "BPG",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "DL",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "GL",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "KMStart",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "KR",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "MS",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "MaxSpeed",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "MultipleJump",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "OWB1",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "OWB2",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "RelativeSpeed",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "Rhythm",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "Scamper",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "Skip",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "WB3",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "ZB",
                table: "BaseEvent");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RealizationId",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    OWB1 = table.Column<float>(nullable: false),
                    OWB2 = table.Column<float>(nullable: false),
                    WB3 = table.Column<float>(nullable: false),
                    ZB = table.Column<float>(nullable: false),
                    KR = table.Column<float>(nullable: false),
                    GL = table.Column<float>(nullable: false),
                    DL = table.Column<float>(nullable: false),
                    Rhythm = table.Column<float>(nullable: false),
                    RelativeSpeed = table.Column<float>(nullable: false),
                    MaxSpeed = table.Column<float>(nullable: false),
                    Skip = table.Column<float>(nullable: false),
                    MultipleJump = table.Column<float>(nullable: false),
                    MS = table.Column<float>(nullable: false),
                    BPG = table.Column<float>(nullable: false),
                    KMStart = table.Column<float>(nullable: false),
                    Scamper = table.Column<float>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEvent_PlanId",
                table: "BaseEvent",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEvent_RealizationId",
                table: "BaseEvent",
                column: "RealizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEvent_Workout_PlanId",
                table: "BaseEvent",
                column: "PlanId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEvent_Workout_RealizationId",
                table: "BaseEvent",
                column: "RealizationId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEvent_Workout_PlanId",
                table: "BaseEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEvent_Workout_RealizationId",
                table: "BaseEvent");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropIndex(
                name: "IX_BaseEvent_PlanId",
                table: "BaseEvent");

            migrationBuilder.DropIndex(
                name: "IX_BaseEvent_RealizationId",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "BaseEvent");

            migrationBuilder.DropColumn(
                name: "RealizationId",
                table: "BaseEvent");

            migrationBuilder.AddColumn<float>(
                name: "BPG",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DL",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "GL",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "KMStart",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "KR",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MS",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxSpeed",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MultipleJump",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OWB1",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OWB2",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RelativeSpeed",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rhythm",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Scamper",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Skip",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WB3",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ZB",
                table: "BaseEvent",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FullWorkout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanId = table.Column<int>(nullable: true),
                    RealizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FullWorkout_BaseEvent_PlanId",
                        column: x => x.PlanId,
                        principalTable: "BaseEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FullWorkout_BaseEvent_RealizationId",
                        column: x => x.RealizationId,
                        principalTable: "BaseEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FullWorkout_PlanId",
                table: "FullWorkout",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_FullWorkout_RealizationId",
                table: "FullWorkout",
                column: "RealizationId");
        }
    }
}
