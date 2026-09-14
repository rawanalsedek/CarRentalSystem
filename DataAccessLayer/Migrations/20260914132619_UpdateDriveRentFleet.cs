using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDriveRentFleet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Luxury" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Year" },
                values: new object[] { "BMW", 1, "https://images.unsplash.com/photo-1555215695-3004980ad54e?auto=format&fit=crop&w=900&q=85", "5 Series", 95m, 2024 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Year" },
                values: new object[] { "Mercedes", 5, "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?auto=format&fit=crop&w=900&q=85", "C Class", 120m, 2024 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "ImageUrl", "Model", "PricePerDay", "Year" },
                values: new object[] { "Toyota", "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?auto=format&fit=crop&w=900&q=85", "RAV4", 65m, 2024 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Status", "Year" },
                values: new object[] { "Audi", 5, "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?auto=format&fit=crop&w=900&q=85", "A6", 110m, 1, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Status" },
                values: new object[] { "BMW", 2, "https://images.unsplash.com/photo-1551830820-330a71b99659?auto=format&fit=crop&w=900&q=85", "X5", 105m, 0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Status", "Year" },
                values: new object[] { "Toyota", 1, "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?auto=format&fit=crop&w=900&q=85", "Camry", 55m, 0, 2024 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Year" },
                values: new object[] { "Toyota", 4, "https://images.unsplash.com/photo-1623869675781-80aa31012a5a?auto=format&fit=crop&w=800&q=80", "Corolla", 45m, 2022 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Year" },
                values: new object[] { "Honda", 1, "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?auto=format&fit=crop&w=800&q=80", "Civic", 55m, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "ImageUrl", "Model", "PricePerDay", "Year" },
                values: new object[] { "BMW", "https://images.unsplash.com/photo-1555215695-3004980ad54e?auto=format&fit=crop&w=800&q=80", "X5", 120m, 2021 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Status", "Year" },
                values: new object[] { "Ford", 3, "https://images.unsplash.com/photo-1584345604476-8ec5e12dc395?auto=format&fit=crop&w=800&q=80", "Mustang", 150m, 0, 2020 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Status" },
                values: new object[] { "Hyundai", 1, "https://images.unsplash.com/photo-1619767886558-efdc259cde1a?auto=format&fit=crop&w=800&q=80", "Elantra", 50m, 2 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Status", "Year" },
                values: new object[] { "Chevrolet", 2, "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?auto=format&fit=crop&w=800&q=80", "Tahoe", 110m, 1, 2022 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
