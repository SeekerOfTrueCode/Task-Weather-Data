using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Technical_Task.Data.Migrations
{
    public partial class AppDataInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TTCountry",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTCountry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TTCity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TTCity_TTCountry_CountryId",
                        column: x => x.CountryId,
                        principalTable: "TTCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TTWeatherOfTheDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateUtc = table.Column<DateTime>(nullable: false),
                    ForecastMessage = table.Column<string>(nullable: true),
                    Cloudiness = table.Column<double>(nullable: false),
                    WindSpeed = table.Column<double>(nullable: false),
                    Humidity = table.Column<double>(nullable: false),
                    RainChance = table.Column<double>(nullable: false),
                    Pressure = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedDateUtc = table.Column<DateTime>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    CityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTWeatherOfTheDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TTWeatherOfTheDay_TTCity_CityId",
                        column: x => x.CityId,
                        principalTable: "TTCity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TTWeatherTemperatureOfTheDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayTimeUtc = table.Column<DateTime>(nullable: false),
                    TemperatureC = table.Column<double>(nullable: false),
                    WeatherOfTheDayId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTWeatherTemperatureOfTheDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TTWeatherTemperatureOfTheDay_TTWeatherOfTheDay_WeatherOfTheDayId",
                        column: x => x.WeatherOfTheDayId,
                        principalTable: "TTWeatherOfTheDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TTCity_CountryId",
                table: "TTCity",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TTWeatherOfTheDay_CityId",
                table: "TTWeatherOfTheDay",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TTWeatherTemperatureOfTheDay_WeatherOfTheDayId",
                table: "TTWeatherTemperatureOfTheDay",
                column: "WeatherOfTheDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TTWeatherTemperatureOfTheDay");

            migrationBuilder.DropTable(
                name: "TTWeatherOfTheDay");

            migrationBuilder.DropTable(
                name: "TTCity");

            migrationBuilder.DropTable(
                name: "TTCountry");
        }
    }
}
