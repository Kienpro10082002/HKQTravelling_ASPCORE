using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKQTravelling.Migrations
{
    /// <inheritdoc />
    public partial class RelationShipRuleAndTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tours_rules_RULE_ID",
                table: "tours");

            migrationBuilder.DropIndex(
                name: "IX_tours_RULE_ID",
                table: "tours");

            migrationBuilder.DropColumn(
                name: "RULE_ID",
                table: "tours");

            migrationBuilder.RenameColumn(
                name: "RULE_ID",
                table: "rules",
                newName: "TourId");

            migrationBuilder.AlterColumn<long>(
                name: "TourId",
                table: "rules",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_rules_tours_TourId",
                table: "rules",
                column: "TourId",
                principalTable: "tours",
                principalColumn: "TOUR_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rules_tours_TourId",
                table: "rules");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "rules",
                newName: "RULE_ID");

            migrationBuilder.AddColumn<long>(
                name: "RULE_ID",
                table: "tours",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "RULE_ID",
                table: "rules",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_tours_RULE_ID",
                table: "tours",
                column: "RULE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tours_rules_RULE_ID",
                table: "tours",
                column: "RULE_ID",
                principalTable: "rules",
                principalColumn: "RULE_ID");
        }
    }
}
