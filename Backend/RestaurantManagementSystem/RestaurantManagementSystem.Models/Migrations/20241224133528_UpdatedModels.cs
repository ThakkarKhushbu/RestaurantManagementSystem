using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace RestaurantManagementSystem.Models.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            _ = migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Tables",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            _ = migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            _ = migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Reservations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            _ = migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Reservations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            _ = migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Location", "SeatingCapacity", "TableNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(2883), true, "Main Hall", 4, 1, new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3025) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3161), true, "Window Side", 6, 2, new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3161) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3163), true, "Outdoor Patio", 8, 3, new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3163) }
                });

            _ = migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "ContactNumber", "CreatedAt", "CustomerName", "FromTime", "GuestCount", "ReservationDate", "Status", "TableId", "ToTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), "+1234567890", new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8172), "John Doe", new TimeSpan(0, 18, 0, 0, 0), 4, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("11111111-1111-1111-1111-111111111111"), new TimeSpan(0, 20, 0, 0, 0), new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8315) },
                    { new Guid("a2222222-2222-2222-2222-222222222222"), "+1234567891", new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8480), "Jane Smith", new TimeSpan(0, 19, 0, 0, 0), 6, new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("22222222-2222-2222-2222-222222222222"), new TimeSpan(0, 21, 0, 0, 0), new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8481) }
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables",
                column: "TableNumber",
                unique: true);

            _ = migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            _ = migrationBuilder.DropIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables");

            _ = migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-111111111111"));

            _ = migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: new Guid("a2222222-2222-2222-2222-222222222222"));

            _ = migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            _ = migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            _ = migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            _ = migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            _ = migrationBuilder.AlterColumn<DateOnly>(
                name: "ReservationDate",
                table: "Reservations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            _ = migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            _ = migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            _ = migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
