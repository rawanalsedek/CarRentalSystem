using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ExpandCarFleet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "CategoryId", "ImageUrl", "Model", "PricePerDay", "Status", "Year" },
                values: new object[,]
                {
                    { 7, "Toyota", 1, "https://images.unsplash.com/photo-1623869675781-80aa31012a5a?auto=format&fit=crop&w=900&q=85", "Corolla", 45m, 0, 2023 },
                    { 8, "Toyota", 4, "https://images.unsplash.com/photo-1541899481282-d31b556d089d?auto=format&fit=crop&w=900&q=85", "Yaris", 35m, 0, 2024 },
                    { 9, "Toyota", 2, "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?auto=format&fit=crop&w=900&q=85", "Land Cruiser", 145m, 0, 2024 },
                    { 10, "BMW", 1, "https://images.unsplash.com/photo-1617531653332-bd46c24f2068?auto=format&fit=crop&w=900&q=85", "3 Series", 85m, 0, 2024 },
                    { 11, "BMW", 2, "https://images.unsplash.com/photo-1563720223185-11003d516935?auto=format&fit=crop&w=900&q=85", "X3", 90m, 0, 2024 },
                    { 12, "BMW", 5, "https://images.unsplash.com/photo-1617814076367-b759c7d7e738?auto=format&fit=crop&w=900&q=85", "7 Series", 180m, 0, 2024 },
                    { 13, "Mercedes", 5, "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?auto=format&fit=crop&w=900&q=85", "E-Class", 140m, 0, 2024 },
                    { 14, "Mercedes", 5, "https://images.unsplash.com/photo-1614162692292-7ac56d7f371e?auto=format&fit=crop&w=900&q=85", "S-Class", 220m, 0, 2023 },
                    { 15, "Mercedes", 2, "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?auto=format&fit=crop&w=900&q=85", "GLC", 125m, 0, 2024 },
                    { 16, "Mercedes", 2, "https://images.unsplash.com/photo-1502877338535-766e1452684a?auto=format&fit=crop&w=900&q=85", "GLE", 155m, 0, 2024 },
                    { 17, "Audi", 1, "https://images.unsplash.com/photo-1542282088-fe8426682b8f?auto=format&fit=crop&w=900&q=85", "A3", 70m, 0, 2024 },
                    { 18, "Audi", 1, "https://images.unsplash.com/photo-1492144534655-ae79c964c9d7?auto=format&fit=crop&w=900&q=85", "A4", 85m, 0, 2024 },
                    { 19, "Audi", 2, "https://images.unsplash.com/photo-1511919884226-fd3cad34687c?auto=format&fit=crop&w=900&q=85", "Q3", 80m, 0, 2023 },
                    { 20, "Audi", 2, "https://images.unsplash.com/photo-1542362567-b07e54358753?auto=format&fit=crop&w=900&q=85", "Q5", 100m, 0, 2024 },
                    { 21, "Hyundai", 1, "https://images.unsplash.com/photo-1619767886558-efdc259cde1a?auto=format&fit=crop&w=900&q=85", "Elantra", 42m, 0, 2024 },
                    { 22, "Hyundai", 1, "https://images.unsplash.com/photo-1489824904134-933ca8c2ff1d?auto=format&fit=crop&w=900&q=85", "Sonata", 50m, 0, 2023 },
                    { 23, "Hyundai", 2, "https://images.unsplash.com/photo-1494976388531-d1058494cdd8?auto=format&fit=crop&w=900&q=85", "Tucson", 58m, 0, 2024 },
                    { 24, "Hyundai", 2, "https://images.unsplash.com/photo-1525609004556-c46c7d6cf023?auto=format&fit=crop&w=900&q=85", "Santa Fe", 68m, 0, 2024 },
                    { 25, "Hyundai", 4, "https://images.unsplash.com/photo-1493238792000-8113da705763?auto=format&fit=crop&w=900&q=85", "Accent", 32m, 0, 2023 },
                    { 26, "Kia", 1, "https://images.unsplash.com/photo-1544636331-e26879cd4d9b?auto=format&fit=crop&w=900&q=85", "Cerato", 40m, 0, 2023 },
                    { 27, "Kia", 2, "https://images.unsplash.com/photo-1503376780353-7e6692767b70?auto=format&fit=crop&w=900&q=85", "Sportage", 55m, 0, 2024 },
                    { 28, "Kia", 2, "https://images.unsplash.com/photo-1492144534655-ae79c964c9d7?auto=format&fit=crop&w=900&q=85", "Sorento", 70m, 0, 2024 },
                    { 29, "Kia", 1, "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?auto=format&fit=crop&w=900&q=85", "K5", 52m, 0, 2024 },
                    { 30, "Kia", 4, "https://images.unsplash.com/photo-1541899481282-d31b556d089d?auto=format&fit=crop&w=900&q=85", "Picanto", 28m, 1, 2023 },
                    { 31, "Ford", 4, "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?auto=format&fit=crop&w=900&q=85", "Focus", 38m, 0, 2023 },
                    { 32, "Ford", 1, "https://images.unsplash.com/photo-1489824904134-933ca8c2ff1d?auto=format&fit=crop&w=900&q=85", "Fusion", 45m, 2, 2022 },
                    { 33, "Ford", 3, "https://images.unsplash.com/photo-1584345604476-8ec5e12dc395?auto=format&fit=crop&w=900&q=85", "Mustang", 150m, 0, 2024 },
                    { 34, "Ford", 2, "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?auto=format&fit=crop&w=900&q=85", "Explorer", 85m, 0, 2024 },
                    { 35, "Ford", 2, "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?auto=format&fit=crop&w=900&q=85", "Escape", 60m, 0, 2023 },
                    { 36, "Honda", 1, "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?auto=format&fit=crop&w=900&q=85", "Civic", 48m, 0, 2024 },
                    { 37, "Honda", 1, "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?auto=format&fit=crop&w=900&q=85", "Accord", 55m, 0, 2024 },
                    { 38, "Honda", 2, "https://images.unsplash.com/photo-1551830820-330a71b99659?auto=format&fit=crop&w=900&q=85", "CR-V", 62m, 0, 2024 },
                    { 39, "Honda", 4, "https://images.unsplash.com/photo-1623869675781-80aa31012a5a?auto=format&fit=crop&w=900&q=85", "City", 36m, 2, 2023 },
                    { 40, "Honda", 2, "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?auto=format&fit=crop&w=900&q=85", "HR-V", 52m, 0, 2024 },
                    { 41, "Nissan", 4, "https://images.unsplash.com/photo-1541899481282-d31b556d089d?auto=format&fit=crop&w=900&q=85", "Sunny", 30m, 1, 2023 },
                    { 42, "Nissan", 1, "https://images.unsplash.com/photo-1542282088-fe8426682b8f?auto=format&fit=crop&w=900&q=85", "Sentra", 40m, 0, 2024 },
                    { 43, "Nissan", 1, "https://images.unsplash.com/photo-1617531653332-bd46c24f2068?auto=format&fit=crop&w=900&q=85", "Altima", 48m, 0, 2024 },
                    { 44, "Nissan", 2, "https://images.unsplash.com/photo-1502877338535-766e1452684a?auto=format&fit=crop&w=900&q=85", "X-Trail", 65m, 0, 2024 },
                    { 45, "Nissan", 2, "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?auto=format&fit=crop&w=900&q=85", "Patrol", 160m, 0, 2024 },
                    { 46, "Chevrolet", 1, "https://images.unsplash.com/photo-1489824904134-933ca8c2ff1d?auto=format&fit=crop&w=900&q=85", "Malibu", 46m, 0, 2023 },
                    { 47, "Chevrolet", 4, "https://images.unsplash.com/photo-1493238792000-8113da705763?auto=format&fit=crop&w=900&q=85", "Cruze", 38m, 0, 2022 },
                    { 48, "Chevrolet", 2, "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?auto=format&fit=crop&w=900&q=85", "Tahoe", 110m, 0, 2024 },
                    { 49, "Chevrolet", 2, "https://images.unsplash.com/photo-1525609004556-c46c7d6cf023?auto=format&fit=crop&w=900&q=85", "Captiva", 58m, 0, 2023 },
                    { 50, "Chevrolet", 2, "https://images.unsplash.com/photo-1494976388531-d1058494cdd8?auto=format&fit=crop&w=900&q=85", "Equinox", 62m, 0, 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 50);
        }
    }
}
