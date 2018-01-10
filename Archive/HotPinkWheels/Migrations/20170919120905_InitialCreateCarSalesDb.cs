using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HotPinkWheels.Migrations
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
