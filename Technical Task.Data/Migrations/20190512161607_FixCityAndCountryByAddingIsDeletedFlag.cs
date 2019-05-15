using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Technical_Task.Data.Migrations
{
    public partial class FixCityAndCountryByAddingIsDeletedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateUtc",
                table: "TTCountry",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TTCountry",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateUtc",
                table: "TTCity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TTCity",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDateUtc",
                table: "TTCountry");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TTCountry");

            migrationBuilder.DropColumn(
                name: "DeletedDateUtc",
                table: "TTCity");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TTCity");
        }
    }
}
