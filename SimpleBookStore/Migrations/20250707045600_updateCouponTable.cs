using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBookStore.Migrations
{
    /// <inheritdoc />
    public partial class updateCouponTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 5, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7297), new DateTime(2025, 7, 7, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7296) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 5, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7301), new DateTime(2025, 7, 7, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 5, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7303), new DateTime(2025, 7, 7, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7302) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 5, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7305), new DateTime(2025, 7, 7, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7305) });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 5, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7307), new DateTime(2025, 7, 7, 4, 55, 58, 405, DateTimeKind.Utc).AddTicks(7307) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913), new DateTime(2025, 7, 7, 12, 55, 58, 405, DateTimeKind.Unspecified).AddTicks(6913) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 7, 12, 57, 58, 405, DateTimeKind.Unspecified).AddTicks(6913), new DateTime(2025, 7, 7, 12, 57, 58, 405, DateTimeKind.Unspecified).AddTicks(6913) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 7, 12, 58, 58, 405, DateTimeKind.Unspecified).AddTicks(6913), new DateTime(2025, 7, 7, 12, 58, 58, 405, DateTimeKind.Unspecified).AddTicks(6913) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 7, 12, 59, 58, 405, DateTimeKind.Unspecified).AddTicks(6913), new DateTime(2025, 7, 7, 12, 59, 58, 405, DateTimeKind.Unspecified).AddTicks(6913) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 7, 13, 0, 58, 405, DateTimeKind.Unspecified).AddTicks(6913), new DateTime(2025, 7, 7, 13, 0, 58, 405, DateTimeKind.Unspecified).AddTicks(6913) });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Code",
                table: "Coupons",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_Code",
                table: "Coupons");

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
    }
}
