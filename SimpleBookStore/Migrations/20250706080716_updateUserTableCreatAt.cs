using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBookStore.Migrations
{
    /// <inheritdoc />
    public partial class updateUserTableCreatAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 4, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3604), new DateTime(2025, 7, 6, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3603) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 4, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3619), new DateTime(2025, 7, 6, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3618) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 4, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3621), new DateTime(2025, 7, 6, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3621) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 4, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3623), new DateTime(2025, 7, 6, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3623) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 4, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3625), new DateTime(2025, 7, 6, 8, 7, 16, 130, DateTimeKind.Utc).AddTicks(3625) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386), new DateTime(2025, 7, 6, 16, 7, 16, 130, DateTimeKind.Unspecified).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 16, 9, 16, 130, DateTimeKind.Unspecified).AddTicks(3386), new DateTime(2025, 7, 6, 16, 9, 16, 130, DateTimeKind.Unspecified).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 16, 10, 16, 130, DateTimeKind.Unspecified).AddTicks(3386), new DateTime(2025, 7, 6, 16, 10, 16, 130, DateTimeKind.Unspecified).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 16, 11, 16, 130, DateTimeKind.Unspecified).AddTicks(3386), new DateTime(2025, 7, 6, 16, 11, 16, 130, DateTimeKind.Unspecified).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 16, 12, 16, 130, DateTimeKind.Unspecified).AddTicks(3386), new DateTime(2025, 7, 6, 16, 12, 16, 130, DateTimeKind.Unspecified).AddTicks(3386) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 3, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 7, 5, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5390) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 3, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5393), new DateTime(2025, 7, 5, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5393) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 3, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5395), new DateTime(2025, 7, 5, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5395) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 3, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5398), new DateTime(2025, 7, 5, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5397) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 3, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5399), new DateTime(2025, 7, 5, 13, 2, 33, 975, DateTimeKind.Utc).AddTicks(5399) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188), new DateTime(2025, 7, 5, 21, 2, 33, 975, DateTimeKind.Unspecified).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 21, 4, 33, 975, DateTimeKind.Unspecified).AddTicks(5188), new DateTime(2025, 7, 5, 21, 4, 33, 975, DateTimeKind.Unspecified).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 21, 5, 33, 975, DateTimeKind.Unspecified).AddTicks(5188), new DateTime(2025, 7, 5, 21, 5, 33, 975, DateTimeKind.Unspecified).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 21, 6, 33, 975, DateTimeKind.Unspecified).AddTicks(5188), new DateTime(2025, 7, 5, 21, 6, 33, 975, DateTimeKind.Unspecified).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 21, 7, 33, 975, DateTimeKind.Unspecified).AddTicks(5188), new DateTime(2025, 7, 5, 21, 7, 33, 975, DateTimeKind.Unspecified).AddTicks(5188) });
        }
    }
}
