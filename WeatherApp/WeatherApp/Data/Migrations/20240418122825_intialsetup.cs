using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class intialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<float>(type: "real", nullable: false),
                    Lon = table.Column<float>(type: "real", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: true),
                    TempFeelsLike = table.Column<float>(type: "real", nullable: true),
                    Temp = table.Column<float>(type: "real", nullable: true),
                    WindSpeed = table.Column<float>(type: "real", nullable: true),
                    WindDir = table.Column<float>(type: "real", nullable: true),
                    WeatherIcon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");
        }
    }
}
