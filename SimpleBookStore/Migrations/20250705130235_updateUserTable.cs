using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBookStore.Migrations
{
    /// <inheritdoc />
    public partial class updateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 2, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7218), new DateTime(2025, 7, 4, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7217) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 2, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7221), new DateTime(2025, 7, 4, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7221) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 2, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7224), new DateTime(2025, 7, 4, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7223) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 2, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7226), new DateTime(2025, 7, 4, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7226) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 2, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7229), new DateTime(2025, 7, 4, 16, 49, 31, 860, DateTimeKind.Utc).AddTicks(7228) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032), new DateTime(2025, 7, 5, 0, 49, 31, 860, DateTimeKind.Utc).AddTicks(7032) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 0, 51, 31, 860, DateTimeKind.Utc).AddTicks(7032), new DateTime(2025, 7, 5, 0, 51, 31, 860, DateTimeKind.Utc).AddTicks(7032) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 0, 52, 31, 860, DateTimeKind.Utc).AddTicks(7032), new DateTime(2025, 7, 5, 0, 52, 31, 860, DateTimeKind.Utc).AddTicks(7032) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 0, 53, 31, 860, DateTimeKind.Utc).AddTicks(7032), new DateTime(2025, 7, 5, 0, 53, 31, 860, DateTimeKind.Utc).AddTicks(7032) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 0, 54, 31, 860, DateTimeKind.Utc).AddTicks(7032), new DateTime(2025, 7, 5, 0, 54, 31, 860, DateTimeKind.Utc).AddTicks(7032) });
        }
    }
}
