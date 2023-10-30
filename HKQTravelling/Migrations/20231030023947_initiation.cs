using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKQTravelling.Migrations
{
    /// <inheritdoc />
    public partial class initiation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    DIS_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DIS_PER = table.Column<double>(type: "float", nullable: true),
                    DIS_NAME = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DIS_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discounts", x => x.DIS_ID);
                });

            migrationBuilder.CreateTable(
                name: "endLocations",
                columns: table => new
                {
                    END_LOCATION_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    END_LOCATION_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endLocations", x => x.END_LOCATION_ID);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    ROLE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROLE_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.ROLE_ID);
                });

            migrationBuilder.CreateTable(
                name: "rules",
                columns: table => new
                {
                    RULE_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRICE_INCLUDE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PRICE_NOT_INCLUDE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SURCHARGE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CANCLE_CHANGE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NOTE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rules", x => x.RULE_ID);
                });

            migrationBuilder.CreateTable(
                name: "startLocations",
                columns: table => new
                {
                    START_LOCATION_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    START_LOCATION_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_startLocations", x => x.START_LOCATION_ID);
                });

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
                name: "users",
                columns: table => new
                {
                    USER_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ROLE_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.USER_ID);
                    table.ForeignKey(
                        name: "FK_users_roles_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "roles",
                        principalColumn: "ROLE_ID");
                });

            migrationBuilder.CreateTable(
                name: "tours",
                columns: table => new
                {
                    TOUR_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TOUR_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PRICE = table.Column<int>(type: "int", nullable: true),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUS = table.Column<int>(type: "int", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    REMAINING = table.Column<int>(type: "int", nullable: true),
                    DIS_ID = table.Column<long>(type: "bigint", nullable: true),
                    START_LOCATION_ID = table.Column<long>(type: "bigint", nullable: true),
                    END_LOCATION_ID = table.Column<long>(type: "bigint", nullable: true),
                    RULE_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tours", x => x.TOUR_ID);
                    table.ForeignKey(
                        name: "FK_tours_discounts_DIS_ID",
                        column: x => x.DIS_ID,
                        principalTable: "discounts",
                        principalColumn: "DIS_ID");
                    table.ForeignKey(
                        name: "FK_tours_endLocations_END_LOCATION_ID",
                        column: x => x.END_LOCATION_ID,
                        principalTable: "endLocations",
                        principalColumn: "END_LOCATION_ID");
                    table.ForeignKey(
                        name: "FK_tours_rules_RULE_ID",
                        column: x => x.RULE_ID,
                        principalTable: "rules",
                        principalColumn: "RULE_ID");
                    table.ForeignKey(
                        name: "FK_tours_startLocations_START_LOCATION_ID",
                        column: x => x.START_LOCATION_ID,
                        principalTable: "startLocations",
                        principalColumn: "START_LOCATION_ID");
                });

            migrationBuilder.CreateTable(
                name: "ticketInformations",
                columns: table => new
                {
                    TICKET_INFO_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ADULTS_NUMBER = table.Column<int>(type: "int", nullable: true),
                    KID_NUMBER = table.Column<int>(type: "int", nullable: true),
                    KID_AGE = table.Column<int>(type: "int", nullable: true),
                    TICKET_TYPE_ID = table.Column<int>(type: "int", nullable: true),
                    USER_ID = table.Column<long>(type: "bigint", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "userDetails",
                columns: table => new
                {
                    USER_DETAIL_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GENDER = table.Column<int>(type: "int", nullable: true),
                    BIRTHDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AGE = table.Column<int>(type: "int", nullable: true),
                    SURNAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NI_NUMBER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    USER_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDetails", x => x.USER_DETAIL_ID);
                    table.ForeignKey(
                        name: "FK_userDetails_users_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "users",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateTable(
                name: "tourDays",
                columns: table => new
                {
                    TOUR_DAY_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DAY_NUMBER = table.Column<int>(type: "int", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESTINATION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TIME_SCHEDULE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TOUR_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourDays", x => x.TOUR_DAY_ID);
                    table.ForeignKey(
                        name: "FK_tourDays_tours_TOUR_ID",
                        column: x => x.TOUR_ID,
                        principalTable: "tours",
                        principalColumn: "TOUR_ID");
                });

            migrationBuilder.CreateTable(
                name: "tourImages",
                columns: table => new
                {
                    IMAGE_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TOUR_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourImages", x => x.IMAGE_ID);
                    table.ForeignKey(
                        name: "FK_tourImages_tours_TOUR_ID",
                        column: x => x.TOUR_ID,
                        principalTable: "tours",
                        principalColumn: "TOUR_ID");
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    BOOKING_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOOKING_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TICKET_INFO_ID = table.Column<long>(type: "bigint", nullable: true),
                    TOUR_ID = table.Column<long>(type: "bigint", nullable: true),
                    USER_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.BOOKING_ID);
                    table.ForeignKey(
                        name: "FK_bookings_ticketInformations_TICKET_INFO_ID",
                        column: x => x.TICKET_INFO_ID,
                        principalTable: "ticketInformations",
                        principalColumn: "TICKET_INFO_ID");
                    table.ForeignKey(
                        name: "FK_bookings_tours_TOUR_ID",
                        column: x => x.TOUR_ID,
                        principalTable: "tours",
                        principalColumn: "TOUR_ID");
                    table.ForeignKey(
                        name: "FK_bookings_users_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "users",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    PAYMENT_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMOUNT = table.Column<int>(type: "int", nullable: true),
                    PAYMENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BOOKING_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.PAYMENT_ID);
                    table.ForeignKey(
                        name: "FK_payments_bookings_BOOKING_ID",
                        column: x => x.BOOKING_ID,
                        principalTable: "bookings",
                        principalColumn: "BOOKING_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_TICKET_INFO_ID",
                table: "bookings",
                column: "TICKET_INFO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_TOUR_ID",
                table: "bookings",
                column: "TOUR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_USER_ID",
                table: "bookings",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_endLocations_END_LOCATION_NAME",
                table: "endLocations",
                column: "END_LOCATION_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_BOOKING_ID",
                table: "payments",
                column: "BOOKING_ID");

            migrationBuilder.CreateIndex(
                name: "IX_roles_ROLE_NAME",
                table: "roles",
                column: "ROLE_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_startLocations_START_LOCATION_NAME",
                table: "startLocations",
                column: "START_LOCATION_NAME",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_tourDays_TOUR_ID",
                table: "tourDays",
                column: "TOUR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tourImages_TOUR_ID",
                table: "tourImages",
                column: "TOUR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tours_DIS_ID",
                table: "tours",
                column: "DIS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tours_END_LOCATION_ID",
                table: "tours",
                column: "END_LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tours_RULE_ID",
                table: "tours",
                column: "RULE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tours_START_LOCATION_ID",
                table: "tours",
                column: "START_LOCATION_ID");

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
                name: "IX_userDetails_PHONE_NUMBER",
                table: "userDetails",
                column: "PHONE_NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userDetails_USER_ID",
                table: "userDetails",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_users_ROLE_ID",
                table: "users",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_users_USERNAME",
                table: "users",
                column: "USERNAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "tourDays");

            migrationBuilder.DropTable(
                name: "tourImages");

            migrationBuilder.DropTable(
                name: "userDetails");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "ticketInformations");

            migrationBuilder.DropTable(
                name: "tours");

            migrationBuilder.DropTable(
                name: "tickeTypes");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "endLocations");

            migrationBuilder.DropTable(
                name: "rules");

            migrationBuilder.DropTable(
                name: "startLocations");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
