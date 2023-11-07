using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKQTravelling.Migrations
{
    /// <inheritdoc />
    public partial class updateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_ticketInformations_TICKET_INFO_ID",
                table: "bookings");

            migrationBuilder.DropTable(
                name: "ticketInformations");

            migrationBuilder.DropTable(
                name: "tickeTypes");

            migrationBuilder.DropIndex(
                name: "IX_userDetails_EMAIL",
                table: "userDetails");

            migrationBuilder.DropIndex(
                name: "IX_userDetails_NI_NUMBER",
                table: "userDetails");

            migrationBuilder.DropIndex(
                name: "IX_bookings_TICKET_INFO_ID",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "TICKET_INFO_ID",
                table: "bookings");

            migrationBuilder.AlterColumn<string>(
                name: "NI_NUMBER",
                table: "userDetails",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "userDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "IMAGE_URL",
                table: "userDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DAY_NUMBER",
                table: "tourImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TOTAL_PRICE",
                table: "payments",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NUM_ADULTS",
                table: "bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NUM_KIDS",
                table: "bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NUM_TODDLERS",
                table: "bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PRICE_ADULTS",
                table: "bookings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PRICE_KIDS",
                table: "bookings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PRICE_TODDLERS",
                table: "bookings",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userDetails_EMAIL",
                table: "userDetails",
                column: "EMAIL",
                unique: true,
                filter: "[EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_userDetails_NI_NUMBER",
                table: "userDetails",
                column: "NI_NUMBER",
                unique: true,
                filter: "[NI_NUMBER] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_userDetails_EMAIL",
                table: "userDetails");

            migrationBuilder.DropIndex(
                name: "IX_userDetails_NI_NUMBER",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "IMAGE_URL",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "DAY_NUMBER",
                table: "tourImages");

            migrationBuilder.DropColumn(
                name: "TOTAL_PRICE",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "NUM_ADULTS",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "NUM_KIDS",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "NUM_TODDLERS",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "PRICE_ADULTS",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "PRICE_KIDS",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "PRICE_TODDLERS",
                table: "bookings");

            migrationBuilder.AlterColumn<string>(
                name: "NI_NUMBER",
                table: "userDetails",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "userDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TICKET_INFO_ID",
                table: "bookings",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tickeTypes",
                columns: table => new
                {
                    TICKET_TYPE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TYPE_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickeTypes", x => x.TICKET_TYPE_ID);
                });

            migrationBuilder.CreateTable(
                name: "ticketInformations",
                columns: table => new
                {
                    TICKET_INFO_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TICKET_TYPE_ID = table.Column<int>(type: "int", nullable: true),
                    USER_ID = table.Column<long>(type: "bigint", nullable: true),
                    ADULTS_NUMBER = table.Column<int>(type: "int", nullable: true),
                    KID_AGE = table.Column<int>(type: "int", nullable: true),
                    KID_NUMBER = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketInformations", x => x.TICKET_INFO_ID);
                    table.ForeignKey(
                        name: "FK_ticketInformations_tickeTypes_TICKET_TYPE_ID",
                        column: x => x.TICKET_TYPE_ID,
                        principalTable: "tickeTypes",
                        principalColumn: "TICKET_TYPE_ID");
                    table.ForeignKey(
                        name: "FK_ticketInformations_users_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "users",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_userDetails_EMAIL",
                table: "userDetails",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userDetails_NI_NUMBER",
                table: "userDetails",
                column: "NI_NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookings_TICKET_INFO_ID",
                table: "bookings",
                column: "TICKET_INFO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ticketInformations_TICKET_TYPE_ID",
                table: "ticketInformations",
                column: "TICKET_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ticketInformations_USER_ID",
                table: "ticketInformations",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tickeTypes_TYPE_NAME",
                table: "tickeTypes",
                column: "TYPE_NAME",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_ticketInformations_TICKET_INFO_ID",
                table: "bookings",
                column: "TICKET_INFO_ID",
                principalTable: "ticketInformations",
                principalColumn: "TICKET_INFO_ID");
        }
    }
}
