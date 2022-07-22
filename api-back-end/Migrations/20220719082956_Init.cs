using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_back_end.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArrivingFlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnterDate = table.Column<DateTime>(type: "date", nullable: true),
                    LeaveDate = table.Column<DateTime>(type: "date", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    CurrentStation = table.Column<int>(type: "int", nullable: true),
                    NextStation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrivingFlights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartingFlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnterDate = table.Column<DateTime>(type: "date", nullable: true),
                    LeaveDate = table.Column<DateTime>(type: "date", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    CurrentStation = table.Column<int>(type: "int", nullable: true),
                    NextStation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartingFlights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrepTime = table.Column<int>(type: "int", nullable: false),
                    FlightInStation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArrivingFlights");

            migrationBuilder.DropTable(
                name: "DepartingFlights");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
