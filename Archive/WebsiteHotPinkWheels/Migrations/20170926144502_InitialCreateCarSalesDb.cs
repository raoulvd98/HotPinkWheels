using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebsiteHotPinkWheels.Migrations
{
    public partial class InitialCreateCarSalesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    APK = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AmountofDoors = table.Column<int>(type: "int4", nullable: false),
                    AmountofPreviousOwners = table.Column<int>(type: "int4", nullable: false),
                    AmountofSeats = table.Column<int>(type: "int4", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Enginetype = table.Column<string>(type: "text", nullable: true),
                    Fueltype = table.Column<string>(type: "text", nullable: true),
                    Fuelusage = table.Column<int>(type: "int4", nullable: false),
                    Horsepower = table.Column<int>(type: "int4", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ManufactureYear = table.Column<int>(type: "int4", nullable: false),
                    Mileage = table.Column<int>(type: "int4", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<int>(type: "int4", nullable: false),
                    Topspeed = table.Column<int>(type: "int4", nullable: false),
                    Transmission = table.Column<string>(type: "text", nullable: true),
                    Warranty = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Weight = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Address = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Mail = table.Column<string>(type: "text", nullable: true),
                    PassWord = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
