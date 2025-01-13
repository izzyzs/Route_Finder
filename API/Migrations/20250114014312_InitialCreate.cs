using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarDates",
                columns: table => new
                {
                    ServiceID = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    ExceptionType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarDates", x => new { x.ServiceID, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "CalendarGTFSs",
                columns: table => new
                {
                    ServiceID = table.Column<string>(type: "TEXT", nullable: false),
                    Monday = table.Column<int>(type: "INTEGER", nullable: false),
                    Tuesday = table.Column<int>(type: "INTEGER", nullable: false),
                    Wednesday = table.Column<int>(type: "INTEGER", nullable: false),
                    Thursday = table.Column<int>(type: "INTEGER", nullable: false),
                    Friday = table.Column<int>(type: "INTEGER", nullable: false),
                    Saturday = table.Column<int>(type: "INTEGER", nullable: false),
                    Sunday = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<string>(type: "TEXT", nullable: false),
                    EndDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarGTFSs", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "RouteGTFSs",
                columns: table => new
                {
                    RouteID = table.Column<string>(type: "TEXT", nullable: false),
                    AgencyID = table.Column<string>(type: "TEXT", nullable: false),
                    RouteShortName = table.Column<string>(type: "TEXT", nullable: false),
                    RouteLongName = table.Column<string>(type: "TEXT", nullable: false),
                    RouteType = table.Column<int>(type: "INTEGER", nullable: false),
                    RouteDesc = table.Column<string>(type: "TEXT", nullable: false),
                    RouteUrl = table.Column<string>(type: "TEXT", nullable: false),
                    RouteColor = table.Column<string>(type: "TEXT", nullable: false),
                    RouteTextColor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteGTFSs", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    StopID = table.Column<string>(type: "TEXT", nullable: false),
                    StopName = table.Column<string>(type: "TEXT", nullable: false),
                    StopLat = table.Column<double>(type: "REAL", nullable: false),
                    StopLon = table.Column<double>(type: "REAL", nullable: false),
                    LocationType = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentStation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.StopID);
                });

            migrationBuilder.CreateTable(
                name: "StopTimes",
                columns: table => new
                {
                    TripID = table.Column<string>(type: "TEXT", nullable: false),
                    StopSequence = table.Column<string>(type: "TEXT", nullable: false),
                    StopID = table.Column<string>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopTimes", x => new { x.TripID, x.StopSequence });
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripID = table.Column<string>(type: "TEXT", nullable: false),
                    RouteID = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceID = table.Column<string>(type: "TEXT", nullable: false),
                    TripHeadsign = table.Column<string>(type: "TEXT", nullable: false),
                    DirectionID = table.Column<int>(type: "INTEGER", nullable: false),
                    ShapeID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarDates");

            migrationBuilder.DropTable(
                name: "CalendarGTFSs");

            migrationBuilder.DropTable(
                name: "RouteGTFSs");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "StopTimes");

            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
