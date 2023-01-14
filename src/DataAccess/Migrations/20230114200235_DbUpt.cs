using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DataAccess.Migrations
{
    public partial class DbUpt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "journeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Origin = table.Column<string>(type: "text", nullable: true),
                    Destination = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FlightCarrier = table.Column<string>(type: "text", nullable: true),
                    FlightNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Origin = table.Column<string>(type: "text", nullable: true),
                    Destination = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double", nullable: false),
                    TransportId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightJourney",
                columns: table => new
                {
                    FlightsId = table.Column<int>(type: "int(11)", nullable: false),
                    JourneysId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightJourney", x => new { x.FlightsId, x.JourneysId });
                    table.ForeignKey(
                        name: "FK_FlightJourney_flights_FlightsId",
                        column: x => x.FlightsId,
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightJourney_journeys_JourneysId",
                        column: x => x.JourneysId,
                        principalTable: "journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "journeyflights",
                columns: table => new
                {
                    id_journey = table.Column<int>(type: "int(11)", nullable: false),
                    id_flight = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "journeyflights_flights_fk",
                        column: x => x.id_flight,
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "journeyflights_journeys_fk",
                        column: x => x.id_journey,
                        principalTable: "journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightJourney_JourneysId",
                table: "FlightJourney",
                column: "JourneysId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_TransportId",
                table: "flights",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "journeyflights_flights_fk",
                table: "journeyflights",
                column: "id_flight");

            migrationBuilder.CreateIndex(
                name: "journeyflights_journeys_fk",
                table: "journeyflights",
                column: "id_journey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightJourney");

            migrationBuilder.DropTable(
                name: "journeyflights");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "journeys");

            migrationBuilder.DropTable(
                name: "transports");
        }
    }
}
