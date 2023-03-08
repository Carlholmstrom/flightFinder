using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flightFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightRoutes",
                columns: table => new
                {
                    RouteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartureDestination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalDestination = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRoutes", x => x.RouteId);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adult = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Child = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartureAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    PricesId = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightRouteRouteId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_FlightRoutes_FlightRouteRouteId",
                        column: x => x.FlightRouteRouteId,
                        principalTable: "FlightRoutes",
                        principalColumn: "RouteId");
                    table.ForeignKey(
                        name: "FK_Flights_Prices_PricesId",
                        column: x => x.PricesId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightRouteRouteId",
                table: "Flights",
                column: "FlightRouteRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PricesId",
                table: "Flights",
                column: "PricesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "FlightRoutes");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
