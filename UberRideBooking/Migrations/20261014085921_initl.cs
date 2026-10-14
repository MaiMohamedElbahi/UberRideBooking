using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UberRideBooking.Migrations
{
    /// <inheritdoc />
    public partial class initl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    RideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupLocation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DropOffLocation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Fare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiderId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.RideId);
                    table.ForeignKey(
                        name: "FK_Rides_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rides_Users_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RideId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "RideId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RideId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "RideId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FullName", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John Doe", "hashed_password_123", "+15551234567", "Driver" },
                    { 2, "jane.smith@example.com", "Jane Smith", "hashed_password_456", "+15559876543", "Rider" },
                    { 3, "bob.johnson@example.com", "Bob Johnson", "hashed_password_789", "+15551112233", "Driver" },
                    { 4, "alice.brown@example.com", "Alice Brown", "hashed_password_abc", "+15554445566", "Rider" },
                    { 5, "carlos.m@example.com", "Carlos Mendez", "hashed_password_def", "+15552223344", "Driver" },
                    { 6, "sarah.j@example.com", "Sarah Jenkins", "hashed_password_ghi", "+15553334455", "Rider" },
                    { 7, "david.k@example.com", "David Kim", "hashed_password_jkl", "+15554448899", "Driver" },
                    { 8, "emily.w@example.com", "Emily Watson", "hashed_password_mno", "+15557778811", "Rider" }
                });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "RideId", "DriverId", "DropOffLocation", "Fare", "PickupLocation", "RiderId", "Status" },
                values: new object[,]
                {
                    { 1, 1, "456 Oak Ave, Suburbs", 25.50m, "123 Main St, Downtown", 2, "Completed" },
                    { 2, 3, "101 Maple Blvd, University", 45.00m, "789 Pine Rd, Airport", 4, "Completed" },
                    { 3, 1, "222 Birch Ln, Hospital", 30.00m, "555 Elm St, Mall", 2, "Pending" },
                    { 4, 5, "Golf Club Estates", 120.00m, "Corporate Plaza, Tech District", 6, "Completed" },
                    { 5, 7, "Arts Museum", 20.00m, "Central Station", 8, "Cancelled" },
                    { 6, 7, "123 Main St, Downtown", 28.00m, "456 Oak Ave, Suburbs", 2, "Completed" },
                    { 7, 3, "Hotel District", 35.50m, "Stadium Gate A", 6, "Ongoing" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "Color", "DriverId", "PlateNumber" },
                values: new object[,]
                {
                    { 1, "Silver", 1, "XYZ-1234" },
                    { 2, "Black", 3, "ABC-5678" },
                    { 3, "White", 5, "EV-999X" },
                    { 4, "Blue", 7, "HYU-4421" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "PaymentDate", "PaymentMethod", "RideId", "Status" },
                values: new object[,]
                {
                    { 1, 25.50m, new DateTime(2023, 10, 25, 14, 30, 0, 0, DateTimeKind.Utc), "Credit Card", 1, "Paid" },
                    { 2, 45.00m, new DateTime(2023, 10, 25, 16, 15, 0, 0, DateTimeKind.Utc), "Digital Wallet", 2, "Paid" },
                    { 3, 120.00m, new DateTime(2023, 10, 26, 9, 10, 0, 0, DateTimeKind.Utc), "Credit Card", 4, "Paid" },
                    { 4, 20.00m, new DateTime(2023, 10, 26, 11, 0, 0, 0, DateTimeKind.Utc), "Digital Wallet", 5, "Failed" },
                    { 5, 28.00m, new DateTime(2023, 10, 26, 18, 45, 0, 0, DateTimeKind.Utc), "Cash", 6, "Paid" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingId", "Comment", "RideId", "Score" },
                values: new object[,]
                {
                    { 1, "Excellent service, very clean car.", 1, 5 },
                    { 2, "Smooth ride, arrived on time.", 2, 4 },
                    { 3, "Amazing electric vehicle experience! Very professional driver.", 4, 5 },
                    { 4, "Driver took a very long route and didn't speak English well.", 6, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RideId",
                table: "Payments",
                column: "RideId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RideId",
                table: "Ratings",
                column: "RideId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DriverId",
                table: "Rides",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_RiderId",
                table: "Rides",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DriverId",
                table: "Vehicles",
                column: "DriverId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
