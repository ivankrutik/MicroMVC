using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Products",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryName", "Description", "Image", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, "Закуска", "Жареное или печёное тесто с начинкой. Размер и форма различны, но наиболее распространённая в виде треугольника. Часто подаётся с соусами.", null, "", "Самоса", 15m },
                    { 2L, "Закуска", "Индийское блюдо, приготовленное из кусочков панира, маринованных в специях и приготовленных на гриле в тандыре. Это вегетарианская альтернатива куриной тикке и другим мясным блюдам. Это популярное блюдо широко доступно в Индии и странах с индийской диаспорой", null, "", "Панир тикка", 13.99m },
                    { 3L, "Десерт", "Хлебобулочное изделие из теста с начинкой, которое выпекается или жарится", null, "", "Сладкий пирог", 10.99m },
                    { 4L, "Первое блюдо", "Блюдо быстрого питания из Индии, состоящее из густого овощного карри, которое подается с мягкой булочкой. Он возник в городе Мумбаи.", null, "", "Пав бхаджи", 15m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4L);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Products",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
